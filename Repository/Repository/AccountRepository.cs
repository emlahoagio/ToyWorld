using Contracts;
using Contracts.Services;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private readonly IJwtSupport _jwtSupport;
        private readonly IHasingServices _hasing;

        public AccountRepository(DataContext context, IJwtSupport jwtSupport, IHasingServices hasing) : base(context)
        {
            _jwtSupport = jwtSupport;
            _hasing = hasing;
        }

        public void DisableAccount(Account account)
        {
            account.Status = false;
            Update(account);
        }

        public void EnableAccount(Account account)
        {
            account.Status = true;
            Update(account);
        }

        public async Task<AccountReturnAfterLogin> GetAccountByEmail(string email, bool trackChanges)
        {
            var account = await FindByCondition(account => account.Email == email, trackChanges).SingleOrDefaultAsync();

            if (account == null) return null;

            var result = new AccountReturnAfterLogin
            {
                AccountId = account.Id,
                Avatar = account.Avatar,
                Role = (int)account.Role,
                Token = _jwtSupport.CreateToken((int)account.Role, account.Id),
                Status = (bool)account.Status,
                Biography = account.Biography,
                Email = account.Email,
                Gender = account.Gender,
                Name = account.Name,
                PhoneNumber = account.Phone,
                IsHasPassword = account.Password == null ? false : true
            };

            return result;
        }

        public async Task<AccountReturnAfterLogin> GetAccountByEmail(string email, string password, bool trackChanges)
        {
            var hasing_pw = _hasing.encriptSHA256(password);

            var account = await FindByCondition(account => account.Email == email && account.Password == hasing_pw, trackChanges).SingleOrDefaultAsync();

            if (account == null) return null;

            var result = new AccountReturnAfterLogin
            {
                AccountId = account.Id,
                Avatar = account.Avatar,
                Role = (int)account.Role,
                Token = _jwtSupport.CreateToken((int)account.Role, account.Id),
                Status = (bool)account.Status,
                Biography = account.Biography,
                Email = account.Email,
                Gender = account.Gender,
                Name = account.Name,
                PhoneNumber = account.Phone,
                IsHasPassword = account.Password == null ? false : true
            };

            return result;
        }

        public async Task<Account> GetAccountById(int account_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.Id == account_id, trackChanges).FirstOrDefaultAsync();

            if (result == null) return null;

            return result;
        }

        public async Task<AccountDetail> GetAccountDetail(int account_id, int current_acc_id, bool trackChanges)
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
                NoOfPost = account.Posts.Where(x => x.IsPublic == true).Count(),
                IsFollowed = account.FollowAccountAccountFollows.Where(x => x.AccountId == current_acc_id).Count() == 0 ? false : true
            };

            return result;
        }

        public async Task<Pagination<AccountInList>> GetListAccount(PagingParameters paging, bool trackChanges)
        {
            var accounts = await FindAll(trackChanges).ToListAsync();

            var count = accounts.Count;

            if (count == 0) return null;

            var afterPagingAccount = accounts.Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            var listAccounts = afterPagingAccount.Select(x => new AccountInList
            {
                Avatar = x.Avatar,
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Status = getStatus(x.Status),
                Role = x.Role.Value
            }).ToList();

            var result = new Pagination<AccountInList>
            {
                Count = count,
                Data = listAccounts,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }

        public async Task<Profile> GetProfile(int account_id, bool trackChanges)
        {
            var account = await FindByCondition(x => x.Id == account_id, trackChanges).FirstOrDefaultAsync();

            if (account == null) return null;

            var profile = new Profile
            {
                Avatar = account.Avatar,
                Biography = account.Biography,
                Email = account.Email,
                Gender = account.Gender,
                Id = account.Id,
                Name = account.Name,
                Phone = account.Phone
            };

            return profile;
        }

        public void UpdateAccountToManager(Account account)
        {
            account.Role = 1;
            Update(account);
        }

        public void UpdateAccountToMember(Account account)
        {
            account.Role = 2;
            Update(account);
        }

        private string getStatus(bool? status)
        {
            if (status == true) return "Active";
            return "Disabled";
        }

        public void UpdateNewPassword(Account account, string new_password)
        {
            var hasing_pw = _hasing.encriptSHA256(new_password);
            account.Password = hasing_pw;
            Update(account);
        }

        public async Task<CreatedAccount> GetCreatedAccount(NewAccountParameters param, bool trackChanges)
        {
            var created_account = await FindByCondition(x => x.Name == param.Name && x.Email == param.Email, trackChanges)
                .FirstOrDefaultAsync();

            return new CreatedAccount
            {
                Avatar = created_account.Avatar,
                Id = created_account.Id,
                Name = created_account.Name
            };
        }

        public async Task<IEnumerable<Account>> getListManager()
        {
            var managers = await FindByCondition(x => x.Role == 1, false).ToListAsync();
            return managers;
        }
    }
}
