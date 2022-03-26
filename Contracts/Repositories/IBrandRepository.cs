using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBrandRepository
    {
        Task<Brand> GetBrandByName(string name, bool trackChanges);
        void CreateBrand(Brand brand);
        Task<List<string>> GetBrandCreateContest(bool trackChanges);
        Task<Pagination<BrandInList>> GetBrandToAddFavorite(PagingParameters paging, bool trackChanges);
    }
}
