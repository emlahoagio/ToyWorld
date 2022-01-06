using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ToyRepository : RepositoryBase<Toy>, IToyRepository
    {
        public ToyRepository(DataContext context) :base(context)
        {
        }
    }
}
