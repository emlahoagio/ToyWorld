﻿using Contracts;
using Entities.DataTransferObject;
using Entities.ErrorModel;
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

        /// <summary>
        /// Get list trading post (Role: Manager, Member)
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListTradingPost([FromQuery] PagingParameters paging)
        {
            var result = await _repositoryManager.TradingPost.GetList(paging, trackChanges: false);

            return Ok(result);
        }

        /// <summary>
        /// Get detail of trading post to update (Role: Manager, Member)
        /// </summary>
        /// <param name="tradingpost_id">Id of trading post need to update</param>
        /// <returns></returns>
        [HttpGet]
        [Route("update/{tradingpost_id}")]
        public async Task<IActionResult> GetTradingPostToUpdateInformation(int tradingpost_id)
        {
            var current_login_account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var update_tradingpost = await _repositoryManager.TradingPost.GetTradingPostById(tradingpost_id, trackChanges: false);

            if (update_tradingpost == null) return NotFound();

            if (update_tradingpost.AccountId != current_login_account.Id)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid request");

            var result = new UpdateTradingPost
            {
                Address = update_tradingpost.Address,
                Value = update_tradingpost.Value,
                ToyName = update_tradingpost.ToyName,
                Title = update_tradingpost.Title,
                Content = update_tradingpost.Content,
                Exchange = update_tradingpost.Trading,
                Phone = update_tradingpost.Phone
            };

            return Ok(result);
        }

        /// <summary>
        /// Get information to create new trading post page (Role: Manager, Member)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("init_to_create")]
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
        /// Create new trading post (Role: Manager, Member)
        /// </summary>
        /// <param name="tradingPost"></param>
        /// <param name="group_id">Id of group content trading post</param>
        /// <returns></returns>
        [HttpPost]
        [Route("group/{group_id}")]
        public async Task<IActionResult> CreateTradingPost([FromBody] NewTradingPostParameters tradingPost, int group_id)
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

        /// <summary>
        /// Update trading post to is exchanged (Role: Manager, Member)
        /// </summary>
        /// <param name="tradingpost_id">id of post want to update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("exchanged/{tradingpost_id}")]
        public async Task<IActionResult> UpdateExchangedTradingPost(int tradingpost_id)
        {
            var current_login_account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var update_tradingpost = await _repositoryManager.TradingPost.GetTradingPostById(tradingpost_id, trackChanges: false);

            if (update_tradingpost.AccountId != current_login_account.Id)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid request");

            _repositoryManager.TradingPost.ExchangedTradingPost(update_tradingpost);

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Update all information of trading post
        /// </summary>
        /// <param name="update_infor"></param>
        /// <param name="tradingpost_id">Id of trading post need to update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{tradingpost_id}")]
        public async Task<IActionResult> UpdateAllInformationTradingPost([FromBody]UpdateTradingPostParameters update_infor, int tradingpost_id)
        {
            var current_login_account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var update_tradingpost = await _repositoryManager.TradingPost.GetTradingPostById(tradingpost_id, trackChanges: false);

            if (update_tradingpost.AccountId != current_login_account.Id)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid request");

            if(update_infor.ToyName != update_tradingpost.ToyName)
            {
                var newToyOfTrading = await _repositoryManager.Toy.GetToyByName(update_infor.ToyName, trackChanges: false);
                if(newToyOfTrading == null)
                {
                    update_tradingpost.ToyId = 3;
                }else
                {
                    update_tradingpost.ToyId = newToyOfTrading.Id;
                }
            }

            _repositoryManager.TradingPost.UpdateTradingPost(update_infor, update_tradingpost);

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
    }
}