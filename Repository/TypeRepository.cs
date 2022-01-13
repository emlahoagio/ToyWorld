using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TypeRepository : RepositoryBase<Entities.Models.Type>, ITypeRepository
    {
        public TypeRepository(DataContext context) : base(context)
        {
        }

        public async Task<Entities.Models.Type> GetTypeByName(string name, bool trackChanges)
        {
            var type = await FindByCondition(x => x.Name == name.Trim(), trackChanges).FirstOrDefaultAsync();

            return type;
        }

        public void CreateType(Entities.Models.Type type) => Create(type);
    }
}
