using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FavoriteBrandRepository : RepositoryBase<FavoriteBrand>, IFavoriteBrandRepository
    {
        public FavoriteBrandRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Brand>> GetFavoriteBrand(int account_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.AccountId == account_id, trackChanges).Include(x => x.Brand).ToListAsync();
            if (result.Count == 0) return null;
            return result.Select(x => x.Brand).ToList();
        }

        public bool IsFavoriteBrand(List<Brand> brands, int brand_id)
        {
            foreach (var brand in brands)
            {
                if (brand.Id == brand_id)
                    return true;
            }
            return false;
        }
    }
}
