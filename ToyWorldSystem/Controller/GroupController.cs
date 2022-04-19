using Contracts;
using Entities.ErrorModel;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public GroupController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        #region Get list group
        /// <summary>
        /// Get list name of group for the tool bar on the top (Role: Manager, Member)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListGroup()
        {
            var result = await _repository.Group.GetListGroup(trackChanges: false);

            if (result == null) return NotFound();

            return Ok(result);
        }
        #endregion

        #region Create new group
        /// <summary>
        /// Create new group (Role: Admin)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateGroup(NewGroupParameters param)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);

            if (current_account.Role != 0) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "don't have permission to update");

            var group = new Entities.Models.Group
            {
                Description = param.Description,
                Name = param.Name,
                IsDisable = false
            };
            _repository.Group.Create(group);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Update group information
        /// <summary>
        /// Update information of group (Role: Admin)
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{group_id}")]
        public async Task<IActionResult> UpdateGroup(int group_id, string name, string description)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);

            if (current_account.Role != 0) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "don't have permission to update");

            await _repository.Group.Update(group_id, name, description, trackChanges: false);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Disable or enable group
        /// <summary>
        /// Disable or enable group (Role: Admin)
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="disable_or_enable">0: disable, 1: enable</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{group_id}/disable_or_enable/{disable_or_enable}")]
        public async Task<IActionResult> DisableOrEnableGroup(int group_id, int disable_or_enable)
        {
            if (disable_or_enable != 1 && disable_or_enable != 0)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid update status");

            var current_account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);

            if (current_account.Role != 0) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "don't have permission to update");

            await _repository.Group.DisableOrEnableGroup(group_id, disable_or_enable, trackChanges: false);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion
    }
}
