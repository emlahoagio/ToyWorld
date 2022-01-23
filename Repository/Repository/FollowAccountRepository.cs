using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FollowAccountRepository : RepositoryBase<FollowAccount>, IFollowAccountRepository
    {
        public FollowAccountRepository(DataContext context) : base(context)
        {
        }

        public void CreateFollow(FollowAccount followAccount) => Create(followAccount);

        public void DeleteFollow(FollowAccount followAccount) => Delete(followAccount);

        public Task<FollowAccount> GetFollowAccount(FollowAccount followAccount, bool trackChanges)
        {
            var result = FindByCondition(x => x.AccountId == followAccount.AccountId && x.AccountFollowId == followAccount.AccountFollowId, trackChanges)
                .FirstOrDefaultAsync();

            if (result == null) return null;
            return result;
        }
    }
}
