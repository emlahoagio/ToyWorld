using Contracts;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IFirebaseSupport _firebaseSupport;

        public AccountController(IRepositoryManager repository, IFirebaseSupport firebaseSupport)
        {
            _repository = repository;
            _firebaseSupport = firebaseSupport;
        }

        [AllowAnonymous]
        [HttpPost("loginbyemail")]
        public async Task<ActionResult> loginByEmail(string firebaseToken)
        {
            //init firebase
            _firebaseSupport.initFirebase();
            //get email
            var email = await _firebaseSupport.getEmailFromToken(firebaseToken);
            if(email.Contains("Get email from token error: "))
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, new { firebaseToken = email });
            }
            var account = _repository.Account.getAccountByEmail(email, trackChanges: false);
            if(account == null)
            {
                throw new ErrorDetails(HttpStatusCode.Unauthorized, new { AccountStatus = "This account is not exist in our system" });
            }
            if (!account.Status)
            {
                throw new ErrorDetails(HttpStatusCode.Unauthorized, new { AccountStatus = "This account is disable" });
            }
            return Ok(account);
        }
    }
}
