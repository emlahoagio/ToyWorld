using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<AccountReact>> GetAccountFollowing(int account_id, bool trackChanges)
        {
            var follow_accounts = await FindByCondition(x => x.AccountId == account_id, trackChanges)
                .Include(x => x.AccountFollow)
                .ToListAsync();

            if (follow_accounts == null || follow_accounts.Count == 0) return null;

            var result = follow_accounts.Select(x => new AccountReact
            {
                Avatar = x.AccountFollow.Avatar,
                Id = x.AccountFollow.Id,
                Name = x.AccountFollow.Name
            }).ToList();

            return result;
        }

        public Task<FollowAccount> GetFollowAccount(FollowAccount followAccount, bool trackChanges)
        {
            var result = FindByCondition(x => x.AccountId == followAccount.AccountId && x.AccountFollowId == followAccount.AccountFollowId, trackChanges)
                .FirstOrDefaultAsync();

            if (result == null) return null;
            return result;
        }
    }
}
