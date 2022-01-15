using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ToyRepository : RepositoryBase<Toy>, IToyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repositoryManager;

        public ToyRepository(DataContext context, IConfiguration configuration, IRepositoryManager repositoryManager) :base(context)
        {
            _configuration = configuration;
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<ToyInList>> GetAllToys(ToyParameters toyParameters, bool trackChanges)
        {
            var toys = await FindAll(trackChanges)
                .Include(toy => toy.Brand)
                .Include(toy => toy.Type)
                .Include(toy => toy.Images)
                .OrderBy(toy => toy.Name)
                .Skip((toyParameters.PageNumber -1) * toyParameters.PageSize)
                .Take(toyParameters.PageSize)
                .ToListAsync();

            var result = toys.Select(toy => new ToyInList
            {
                Id = toy.Id,
                Name = toy.Name,
                Price = toy.Price,
                BrandName = toy.Brand.Name,
                TypeName = toy.Type.Name,
                CoverImage = toy.CoverImage
            });

            return result;
        }

        public void CreateToy(Toy toy) => Create(toy);

        public void UpdateToy(Toy toy) => Update(toy);

        public int IdExistToy(string toyName)
        {
            int result = -1;
            var toy = FindByCondition(x => x.Name == toyName.Trim(), trackChanges: false).FirstOrDefault();
            if (toy != null)
            {
                result = toy.Id;
            }
            return result;
        }
    }
}
