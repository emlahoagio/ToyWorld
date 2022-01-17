using Entities.Models;
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
    }
}
