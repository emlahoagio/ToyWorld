using Entities.DataTransferObject;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBrandRepository
    {
        Task<Brand> GetBrandByName(string name, bool trackChanges);
        void CreateBrand(Brand brand);
        Task<List<string>> GetBrandCreateContest(bool trackChanges);
        Task<List<BrandInList>> GetBrandToAddFavorite(int account_id, bool trackChanges);
    }
}
