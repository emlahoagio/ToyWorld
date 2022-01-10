using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ToyRepository : RepositoryBase<Toy>, IToyRepository
    {
        public ToyRepository(DataContext context) :base(context)
        {
        }

        public async Task<IEnumerable<ToyInList>> GetAllToys(ToyParameters toyParameters, bool trackChanges)
        {
            var toys = await FindAll(trackChanges)
                .Include(toy => toy.Brand)
                .Include(toy => toy.Type)
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
                TypeName = toy.Type.Name
            });

            return result;
        }
    }
}
