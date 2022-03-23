using Contracts.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class RateSellerRepository : RepositoryBase<RateSeller>, IRateSellerRepository
    {
        public RateSellerRepository(DataContext context) : base(context)
        {
        }

        public void NewRateSeller(RateSeller rateSeller) => Create(rateSeller);
    }
}
