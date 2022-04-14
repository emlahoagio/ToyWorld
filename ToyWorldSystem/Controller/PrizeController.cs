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
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public PrizeController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        #region Get list prize
        /// <summary>
        /// Get list prize for add to contest and proposal
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> getPrizeList([FromQuery] PagingParameters paging)
        {
            var pagignationPrize_no_image = await _repository.Prize.GetPrize(paging, trackChanges: false);

            var pagignationPrize = await _repository.Image.GetImageForPrizeList(pagignationPrize_no_image, trackChanges: false);

            return Ok(pagignationPrize);
        }
        #endregion

        #region Get infor to update prize
        /// <summary>
        /// Get prize for update, call before update prize (Role: Manager)
        /// </summary>
        /// <param name="prize_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{prize_id}/update")]
        public async Task<IActionResult> GetPrizeForUpdate(int prize_id)
        {
            var prize_no_image = await _repository.Prize.GetUpdatePrize(prize_id, trackChanges: false);

            if (prize_no_image == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "Invalid Prize Id");

            var prize = await _repository.Image.GetImageForPrize(prize_no_image, trackChanges: false);

            return Ok(prize);
        }
        #endregion

        #region Create new prize
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
                Images = new List<Image>{new Image { Url = newPrize.Image }}
            };

            _repository.Prize.CreatePrize(prize);
            await _repository.SaveAsync();

            return (Ok("Save changes success"));
        }
        #endregion

        #region Update prize
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
        #endregion

        #region Disable prize
        /// <summary>
        /// Disable prize (Role: Manager)
        /// </summary>
        /// <param name="prize_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{prize_id}")]
        public async Task<IActionResult> DisablePrize(int prize_id)
        {
            var account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);
            if (account.Role != 1) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to update");

            await _repository.Prize.DisablePrize(prize_id, trackChanges: false);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion
    }
}
