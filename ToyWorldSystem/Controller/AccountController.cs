using Contracts;
using Entities.ErrorModel;
using Entities.RequestFeatures;
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

        /// <summary>
        /// Login by google mail (Role: ALL)
        /// </summary>
        /// <param name="firebaseToken">Token get from firebase</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("loginbyemail")]
        public async Task<IActionResult> LoginByEmail(string firebaseToken)
        {
            //init firebase
            _firebaseSupport.initFirebase();
            //get email
            var email = await _firebaseSupport.getEmailFromToken(firebaseToken);
            if(email.Contains("Get email from token error: "))
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, email);
            }
            var account = await _repository.Account.getAccountByEmail(email, trackChanges: false);
            if(account == null)
            {
                throw new ErrorDetails(HttpStatusCode.Unauthorized, "This account is not exist in our system");
            }
            if (!account.Status)
            {
                throw new ErrorDetails(HttpStatusCode.Unauthorized, "This account is disable" );
            }
            return Ok(account);
        }

        /// <summary>
        /// Login by email and password
        /// </summary>
        /// <param name="unverify_account"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("loginby_system_account")]
        public async Task<IActionResult> LoginByAccountSystem(AccountSystemParameters unverify_account)
        {
            var account = await _repository.Account.getAccountByEmail(unverify_account.Email, unverify_account.Password, trackChanges: false);

            if (account == null) throw new ErrorDetails(HttpStatusCode.Unauthorized, "Invalid username/password!");

            return Ok(account);
        }

        
    }
}
