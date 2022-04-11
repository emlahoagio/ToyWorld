using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FavoriteTypeRepository : RepositoryBase<FavoriteType>, IFavoriteTypeRepository
    {
        public FavoriteTypeRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Entities.Models.Type>> GetFavoriteType(int account_id, bool trackChanges)
        {
            var favorite_type = await FindByCondition(x => x.AccountId == account_id, trackChanges)
                .Include(x => x.Type).ToListAsync();

            if (favorite_type.Count == 0) return null;

            var result = favorite_type.Select(x => x.Type).ToList();

            return result;
        }

        public bool IsFavoriteType(List<Entities.Models.Type> types, int type_id)
        {
            foreach(var type in types)
            {
                if (type.Id == type_id)
                    return true;
            }
            return false;
        }
    }
}
