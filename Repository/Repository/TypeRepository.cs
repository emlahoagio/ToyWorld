using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

            if (type == null)
                type = await FindByCondition(x => x.Id == 5, trackChanges).FirstOrDefaultAsync();

            return type;
        }

        public void CreateType(Entities.Models.Type type) => Create(type);

        public async Task<IEnumerable<string>> GetListName(bool trackChanges)
        {
            var types = await FindAll(trackChanges).ToListAsync();

            var result = types.Select(type => type.Name);

            return result;
        }
    }
}
