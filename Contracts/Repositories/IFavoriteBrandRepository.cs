using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IFavoriteBrandRepository
    {
        public Task<List<Brand>> GetFavoriteBrand(int account_id, bool trackChanges);
        bool IsFavoriteBrand(List<Entities.Models.Brand> brand, int brand_id);
        void Create(FavoriteBrand favorite);
        void Delete(FavoriteBrand favorite);
    }
}
