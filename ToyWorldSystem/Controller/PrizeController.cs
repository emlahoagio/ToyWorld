using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/prizes")]
    [ApiController]
    public class PrizeController : ControllerBase
    {
        private readonly RepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public PrizeController(RepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> getPrizeList([FromQuery]PagingParameters paging)
        {
            var pagignationPrize = await _repository.Prize.GetPrize(paging, trackChanges: false);

            return Ok(pagignationPrize);
        }

        [HttpPost]
        public async Task<IActionResult> createPrize(NewPrizeParameters newPrize)
        {
            var prize = new Prize
            {
                Description = newPrize.Description,
                Name = newPrize.Name,
                Value = newPrize.Value,
                Images = newPrize.Images.Select(x => new Entities.Models.Image
                {
                    Url = x
                }).ToList()
            };

            _repository.Prize.CreatePrize(prize);
            await _repository.SaveAsync();

            return (Ok("Save changes success"));
        }

    }
}
