using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                result = await FindByCondition(brand => brand.Id == 5, trackChanges).FirstOrDefaultAsync();

            return result;
        }

        public void CreateBrand(Brand brand) => Create(brand);
    }
}
