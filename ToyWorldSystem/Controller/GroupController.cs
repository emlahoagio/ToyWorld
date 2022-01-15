using Contracts;
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

        public GroupController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get list name of group for the tool bar on the top (Role: Manager, Member)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListGroup()
        {
            var result = await _repository.Group.getListGroup(trackChanges: false);

            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
