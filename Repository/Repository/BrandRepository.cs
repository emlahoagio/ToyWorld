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
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(DataContext context) : base(context)
        {
        }

        public async Task<Brand> GetBrandByName(string name, bool trackChanges)
        {
            var result = await FindByCondition(brand => brand.Name == name, trackChanges).SingleOrDefaultAsync();

            if (result == null)
                return null;

            return result;
        }

        public async Task<List<string>> GetBrandCreateContest(bool trackChanges)
        {
            var result = await FindAll(trackChanges).OrderBy(x => x.Name).Select(x => x.Name).ToListAsync();

            return result;
        }

        public void CreateBrand(Brand brand) => Create(brand);

        public async Task<Pagination<BrandInList>> GetBrandToAddFavorite(PagingParameters paging, bool trackChanges)
        {
            var result = await FindAll(trackChanges).OrderBy(x => x.Name).ToListAsync();

            var count = result.Count;

            var paging_result = result.Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            var result_list = paging_result.Select(x => new BrandInList
            {
                Name = x.Name,
                Id = x.Id
            });

            var final_result = new Pagination<BrandInList>
            {
                Count = count,
                Data = result_list,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };
            return final_result;
        }
    }
}
