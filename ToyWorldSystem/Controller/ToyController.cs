using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/toys")]
    [ApiController]
    public class ToyController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public ToyController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        [HttpGet]
        public IActionResult GetToys()
        {
            var toys = _repository.Toy.GetAllToys(trackChanges: false);

            return Ok(toys);
        }
    }
}
