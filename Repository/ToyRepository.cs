using Contracts;
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

        public IEnumerable<Toy> GetAllToys(bool trackChanges)
        {
            return FindAll(trackChanges).Include(toy => toy.Brand).Include(toy => toy.Type).OrderBy(toy => toy.Name).ToList();
        }
    }
}
