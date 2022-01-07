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
        private readonly ILoggerManager _logger;

        public ToyController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetToys()
        {
            var toys = _repository.Toy.GetAllToys(trackChanges: false);

            var customizeToys = toys.Select(toy => new ToyInList
            {
                Id = toy.Id,
                Name = toy.Name,
                Price = toy.Price,
                BrandName = toy.Brand.Name,
                TypeName = toy.Type.Name
            });
            return Ok(customizeToys);
        }
    }
}
