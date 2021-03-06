using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task<AccountReturnAfterLogin> GetAccountByEmail(string email, bool trackChanges);
        Task<AccountReturnAfterLogin> GetAccountByEmail(string email, string password, bool trackChanges);
        Task<Pagination<AccountInList>> GetListAccount(PagingParameters paging, bool trackChanges);
        Task<Account> GetAccountById(int account_id, bool trackChanges);
        Task<AccountDetail> GetAccountDetail(int account_id, int current_acc_id, bool trackChanges);
        Task<Profile> GetProfile(int account_id, bool trackChanges);
        Task<CreatedAccount> GetCreatedAccount(NewAccountParameters param, bool trackChanges);
        void DisableAccount(Account account);
        void EnableAccount(Account account);
        void UpdateAccountToManager(Account account);
        void UpdateAccountToMember(Account account);
        void Update(Account account);
        void Create(Account account);
        void UpdateNewPassword(Account account, string new_password);

        Task<IEnumerable<Account>> getListManager();
    }
}
