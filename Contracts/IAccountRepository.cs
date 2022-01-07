using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task<AccountReturnAfterLogin> loginByEmail(string firebaseToken, bool trackChanges);
    }
}
