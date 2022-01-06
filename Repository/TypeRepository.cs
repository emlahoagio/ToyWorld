using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class TypeRepository : RepositoryBase<Entities.Models.Type>, ITypeRepository
    {
        public TypeRepository(DataContext context) : base(context)
        {
        }
    }
}
