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
    [Route("api/contest")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public ContestController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        /// <summary>
        /// Get highlight contest for the home page (Role: Manager, Member)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("highlight")]
        public async Task<IActionResult> getHighlightContest()
        {
            var result = await _repositoryManager.Contest.getHightlightContest(trackChanges: false);

            if(result == null || result.Count() == 0)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest is available");
            }

            return Ok(result);
        }

        /// <summary>
        /// Get contest by group id
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id}")]
        public async Task<IActionResult> getContestByGroup(int group_id, [FromQuery] PagingParameters paging)
        {
            var contest_have_not_prize = await _repositoryManager.Contest.GetContestInGroup(group_id, trackChanges: false, paging);

            var result = await _repositoryManager.PrizeContest.GetPrizeForContest(contest_have_not_prize);

            if (result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest is available");
            }

            return Ok(result);
        }
    }
}
