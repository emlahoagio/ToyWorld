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
        private readonly IFirebaseSupport _firebaseSupport;

        public AccountRepository(DataContext context, IJwtSupport jwtSupport, IFirebaseSupport firebaseSupport) : base(context)
        {
            _jwtSupport = jwtSupport;
            _firebaseSupport = firebaseSupport;
        }

        public async Task<AccountReturnAfterLogin> loginByEmail(string firebaseToken, bool trackChanges)
        {
            //init firebase
            _firebaseSupport.initFirebase();
            //get email from token
            var email = await _firebaseSupport.getEmailFromToken(firebaseToken);
            //check firebase token error:
            if (email.Contains("Get email from token error:"))
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, new {firebaseDecodeError = email });
            }
            //process login
            var account = FindByCondition(account => account.Email.Equals(email), trackChanges).SingleOrDefault();
            //check is email in system
            if (account == null)
            {
                throw new ErrorDetails(HttpStatusCode.Unauthorized, new {AccountStatus = "Account is not exist in TWS" });
            }
            //check account status
            if (account.Status == false)
            {
                throw new ErrorDetails(HttpStatusCode.Unauthorized, new {AccountStatus = "Account is exprise" });
            }
            var result = new AccountReturnAfterLogin
            {
                AccountId = account.Id,
                Avatar = account.Avatar,
                Role = (int)account.Role,
                Token = _jwtSupport.CreateToken((int)account.Role, account.Id)
            };
            return result;
        }
    }
}
