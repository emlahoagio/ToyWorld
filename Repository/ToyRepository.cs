using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ToyRepository : RepositoryBase<Toy>, IToyRepository
    {
        public ToyRepository(DataContext context) :base(context)
        {
        }

        public IEnumerable<ToyInList> GetAllToys(bool trackChanges)
        {
            var toys = FindAll(trackChanges).Include(toy => toy.Brand).Include(toy => toy.Type).OrderBy(toy => toy.Name).ToList();

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
