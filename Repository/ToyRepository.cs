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

        public async Task<Pagination<ToyInList>> GetAllToys(ToyParameters toyParameters, bool trackChanges)
        {
            var toys = await FindByCondition(x => x.Id != 3,trackChanges)
                .Include(toy => toy.Brand)
                .Include(toy => toy.Type).ToListAsync();

            int count = toys.Count();

            var pagingToys = toys.Skip((toyParameters.PageNumber - 1) * toyParameters.PageSize)
                .Take(toyParameters.PageSize)
                .OrderBy(x => x.Name);

            var toysInList = pagingToys.Select(toy => new ToyInList
            {
                Id = toy.Id,
                Name = toy.Name,
                Price = toy.Price,
                BrandName = toy.Brand.Name,
                TypeName = toy.Type.Name,
                CoverImage = toy.CoverImage
            });

            var result = new Pagination<ToyInList>
            {
                Count = count,
                Data = toysInList,
                PageNumber = toyParameters.PageNumber,
                PageSize = toyParameters.PageSize
            };

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

        public async Task<Pagination<ToyInList>> GetToysByType(ToyParameters toyParameters, string typeName, bool trackChanges)
        {
            var toys = await FindByCondition(x => x.Type.Name == typeName && x.Id != 3, trackChanges)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .ToListAsync();

            int count = toys.Count();

            var pagingToys = toys.Skip((toyParameters.PageNumber - 1) * toyParameters.PageSize)
                                .Take(toyParameters.PageSize).OrderBy(x => x.Name);
                

            if (toys == null) return null;

            var toysInList = pagingToys.Select(toy => new ToyInList
            {
                BrandName = toy.Brand.Name,
                Name = toy.Name,
                CoverImage = toy.CoverImage,
                Id = toy.Id,
                Price = toy.Price,
                TypeName = toy.Type.Name
            });

            var result = new Pagination<ToyInList>
            {
                Count = count,
                Data = toysInList,
                PageNumber = toyParameters.PageNumber,
                PageSize = toyParameters.PageSize
            };

            return result;
        }
    }
}
