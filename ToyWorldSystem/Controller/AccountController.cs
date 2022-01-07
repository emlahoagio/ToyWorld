using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public AccountController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpPost("loginbyemail")]
        public IActionResult loginByEmail(string firebaseToken)
        {
            var account = _repository.Account.loginByEmail(firebaseToken, trackChanges: false);

            return Ok(account);
        }
    }
}
