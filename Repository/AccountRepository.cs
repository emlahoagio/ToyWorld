using Contracts;
using Entities.DataTransferObject;
using Entities.ErrorModel;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

        public AccountReturnAfterLogin getAccountByEmail(string email, bool trackChanges)
        {
            var account = FindByCondition(account => account.Email == email, trackChanges).SingleOrDefault();

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
    }
}
