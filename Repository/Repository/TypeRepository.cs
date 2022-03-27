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
    public class TypeRepository : RepositoryBase<Entities.Models.Type>, ITypeRepository
    {
        public TypeRepository(DataContext context) : base(context)
        {
        }

        public async Task<Entities.Models.Type> GetTypeByName(string name, bool trackChanges)
        { 
            var type = await FindByCondition(x => x.Name == name.Trim(), trackChanges).FirstOrDefaultAsync();

            if (type == null)
                return null;

            return type;
        }

        public void CreateType(Entities.Models.Type type) => Create(type);

        public async Task<IEnumerable<string>> GetListName(bool trackChanges)
        {
            var types = await FindAll(trackChanges).ToListAsync();

            var result = types.Select(type => type.Name);

            return result;
        }

        public async Task<List<string>> GetTypeCreateContest(bool trackChanges)
        {
            var types = await FindAll(trackChanges).OrderBy(x => x.Name).Select(x => x.Name).ToListAsync();

            return types;
        }

        public async Task<List<TypeInList>> GetTypeToAddFavorite(int account_id, bool trackChanges)
        {
            var result = await FindAll(trackChanges).Include(x => x.FavoriteTypes).OrderBy(x => x.Name).ToListAsync();

            return result.Select(x => new TypeInList
            {
                Id = x.Id,
                Name = x.Name,
                IsFavorite = x.FavoriteTypes.Where(y => y.AccountId == account_id).Count() > 0
            }).ToList();
        }
    }
}
