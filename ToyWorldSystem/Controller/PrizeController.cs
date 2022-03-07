using Contracts;
using Entities.ErrorModel;
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

        /// <summary>
        /// Get list prize for add to contest and proposal
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> getPrizeList([FromQuery] PagingParameters paging)
        {
            var pagignationPrize = await _repository.Prize.GetPrize(paging, trackChanges: false);

            return Ok(pagignationPrize);
        }

        /// <summary>
        /// Get prize for update, call before update prize (Role: Manager)
        /// </summary>
        /// <param name="prize_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{prize_id}/update")]
        public async Task<IActionResult> GetPrizeForUpdate(int prize_id)
        {
            var prize = await _repository.Prize.GetUpdatePrize(prize_id, trackChanges: false);

            if (prize == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "Invalid Prize Id");

            return Ok(prize);
        }

        /// <summary>
        /// Create new prize
        /// </summary>
        /// <param name="newPrize"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update prize (not contain image, Role: Manager)
        /// </summary>
        /// <param name="param"></param>
        /// <param name="prize_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{prize_id}")]
        public async Task<IActionResult> EditPrize(EditPrizeParameters param, int prize_id)
        {
            await _repository.Prize.UpdatePrize(param, prize_id, trackChanges: false);
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
    }
}
