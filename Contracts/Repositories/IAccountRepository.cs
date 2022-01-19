using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task<AccountReturnAfterLogin> getAccountByEmail(string email, bool trackChanges);
        Task<Account> GetAccountById(int account_id, bool trackChanges);
    }
}
