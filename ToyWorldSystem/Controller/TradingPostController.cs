using Contracts;
using Entities.DataTransferObject;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/trading_posts")]
    [ApiController]
    public class TradingPostController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUserAccessor _userAccessor;

        public TradingPostController(IRepositoryManager repositoryManager, IUserAccessor userAccessor)
        {
            _repositoryManager = repositoryManager;
            _userAccessor = userAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetListTradingPost([FromQuery] PagingParameters paging)
        {
            var result = await _repositoryManager.TradingPost.GetList(paging, trackChanges: false);

            return Ok(result);
        }

        /// <summary>
        /// Get information to create new trading post page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("InitToCreate")]
        public async Task<IActionResult> LoadInformationForCreate()
        {
            var toyNames = await _repositoryManager.Toy.GetNameOfToy(trackChanges: false);

            var currentAccount = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var result = new InitNewTradingPost
            {
                PhoneNumber = currentAccount.Phone,
                ToyNames = toyNames
            };

            return Ok(result);
        }

        /// <summary>
        /// Create new trading post
        /// </summary>
        /// <param name="tradingPost"></param>
        /// <param name="group_id">Id of group content trading post</param>
        /// <returns></returns>
        [HttpPost]
        [Route("group/{group_id}")]
        public async Task<IActionResult> CreateTradingPost([FromBody] NewTradingPost tradingPost, int group_id)
        {
            var account_id = _userAccessor.getAccountId();

            var toy = await _repositoryManager.Toy.GetToyByName(tradingPost.ToyName, trackChanges: false);

            if (toy != null)
            {
                _repositoryManager.TradingPost.CreateTradingPost(tradingPost, group_id, account_id, toy.Id);
            }else
            {
                _repositoryManager.TradingPost.CreateTradingPost(tradingPost, group_id, account_id, 3);
            }

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
    }
}
