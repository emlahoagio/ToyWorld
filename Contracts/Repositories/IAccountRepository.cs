using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task<AccountReturnAfterLogin> getAccountByEmail(string email, bool trackChanges);
        Task<AccountReturnAfterLogin> getAccountByEmail(string email, string password, bool trackChanges);
        Task<Pagination<AccountInList>> GetListAccount(PagingParameters paging, bool trackChanges);
        Task<Account> GetAccountById(int account_id, bool trackChanges);
        Task<AccountDetail> GetAccountDetail(int account_id, bool trackChanges);
        void DisableAccount(Account account);
        void EnableAccount(Account account);
    }
}
