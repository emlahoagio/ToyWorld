using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RateRepository : RepositoryBase<Rate>, IRateRepository
    {
        public RateRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> IsRated(int post_id, int account_id, bool trackChanges)
        {
            var rate = await FindByCondition(x => x.AccountId == account_id && x.PostOfContestId == post_id, trackChanges).FirstOrDefaultAsync();

            return rate == null ? false : true;
        }
    }
}
