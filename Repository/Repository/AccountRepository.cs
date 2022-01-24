using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private readonly IJwtSupport _jwtSupport;

        public AccountRepository(DataContext context, IJwtSupport jwtSupport) : base(context)
        {
            _jwtSupport = jwtSupport;
        }

        public async Task<AccountReturnAfterLogin> getAccountByEmail(string email, bool trackChanges)
        {
            var account = await FindByCondition(account => account.Email == email, trackChanges).SingleOrDefaultAsync();

            if (account == null) return null;

            var result = new AccountReturnAfterLogin
            {
                AccountId = account.Id,
                Avatar = account.Avatar,
                Role = (int)account.Role,
                Token = _jwtSupport.CreateToken((int)account.Role, account.Id),
                Status = (bool)account.Status
            };

            return result;
        }

        public async Task<AccountReturnAfterLogin> getAccountByEmail(string email, string password, bool trackChanges)
        {
            var account = await FindByCondition(account => account.Email == email && account.Password == password, trackChanges).SingleOrDefaultAsync();

            if (account == null) return null;

            var result = new AccountReturnAfterLogin
            {
                AccountId = account.Id,
                Avatar = account.Avatar,
                Role = (int)account.Role,
                Token = _jwtSupport.CreateToken((int)account.Role, account.Id),
                Status = (bool)account.Status
            };

            return result;
        }

        public async Task<Account> GetAccountById(int account_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.Id == account_id, trackChanges).FirstOrDefaultAsync();

            if (result == null) return null;

            return result;
        }

        public async Task<AccountDetail> GetAccountDetail(int account_id, bool trackChanges)
        {
            var account = await FindByCondition(x => x.Id == account_id, trackChanges)
                .Include(x => x.Posts)
                .Include(x => x.FollowAccountAccounts)
                .Include(x => x.FollowAccountAccountFollows)
                .FirstOrDefaultAsync();

            if (account == null) return null;

            var result = new AccountDetail
            {
                Avatar = account.Avatar,
                Biography = account.Biography,
                Name = account.Name,
                NoOfFollower = account.FollowAccountAccountFollows.Count,
                NoOfFollowing = account.FollowAccountAccounts.Count,
                NoOfPost = account.Posts.Count
            };

            return result;
        }
    }
}
