using Contracts;
using Entities.ErrorModel;
using Entities.Models;
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
        private readonly IUserAccessor _userAccessor;

        public ContestController(IRepositoryManager repositoryManager, IUserAccessor userAccessor)
        {
            _repositoryManager = repositoryManager;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Get highlight contest for the home page (Role: Manager, Member)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("highlight")]
        public async Task<IActionResult> GetHighlightContest()
        {
            var result = await _repositoryManager.Contest.getHightlightContest(trackChanges: false);

            if (result == null || result.Count() == 0)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest is available");
            }

            return Ok(result);
        }

        /// <summary>
        /// Check is user attended to the contest (Role: Manager, Member)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{contest_id}/attended")]
        public async Task<IActionResult> CheckAccountInTheContest(int contest_id)
        {
            var account_id = _userAccessor.getAccountId();

            var isInContest = await _repositoryManager.JoinContest.IsJoinedToContest(contest_id, account_id, trackChanges: false);

            return Ok(new {IsJoinedToContest = isInContest});
        }

        /// <summary>
        /// Get contest by group id (Role: Manager, Member)
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id}")]
        public async Task<IActionResult> GetContestByGroup(int group_id, [FromQuery] PagingParameters paging)
        {
            var contest_have_not_prize = await _repositoryManager.Contest.GetContestInGroup(group_id, trackChanges: false, paging);

            var result = await _repositoryManager.PrizeContest.GetPrizeForContest(contest_have_not_prize);

            if (result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest is available");
            }

            return Ok(result);
        }

        /// <summary>
        /// Get information of proposal to create contest (Role: Manager)
        /// </summary>
        /// <param name="proposal_id"></param>
        /// <returns></returns>
        /// <exception cref="ErrorDetails"></exception>
        [HttpGet]
        [Route("create/proposal/{proposal_id}")]
        public async Task<IActionResult> GetProposalToCreateContest(int proposal_id)
        {
            var proposal_information = await _repositoryManager.Proposal.GetInformationToCreateContest(proposal_id, trackChanges: false);

            if (proposal_information == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No proposal matches with id: " + proposal_id);

            return Ok(proposal_information);
        }

        /// <summary>
        /// Create contest (Role: Manager)
        /// </summary>
        /// <param name="param"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("group/{group_id}")]
        public async Task<IActionResult> CreateContest(CreateContestParameters param, int group_id)
        {
            var brand = await _repositoryManager.Brand
                .GetBrandByName(param.BrandName == null ? "Unknow Brand" : param.BrandName, trackChanges: false);
            if (brand == null)
            {
                _repositoryManager.Brand.CreateBrand(new Brand { Name = param.BrandName });
                await _repositoryManager.SaveAsync();
                brand = await _repositoryManager.Brand.GetBrandByName(param.BrandName, trackChanges: false);
            }

            var type = await _repositoryManager.Type
                .GetTypeByName(param.TypeName == null ? "Unknow Type" : param.TypeName, trackChanges: false);
            if (type == null)
            {
                _repositoryManager.Type.CreateType(new Entities.Models.Type { Name = param.TypeName });
                await _repositoryManager.SaveAsync();
                type = await _repositoryManager.Type.GetTypeByName(param.BrandName, trackChanges: false);
            }

            var contest = new Contest
            {
                Title = param.Title,
                Description = param.Description,
                Venue = param.Location,
                CoverImage = param.CoverImage,
                Slogan = param.Slogan,
                IsOnlineContest = param.IsOnlineContest,
                RegisterCost = param.RegisterCost,
                MinRegistration = param.MinRegistration,
                MaxRegistration = param.MaxRegistration,
                StartRegistration = param.StartRegistration,
                EndRegistration = param.EndRegistration,
                StartDate = param.StartDate,
                EndDate = param.EndDate,
                GroupId = group_id,
                ProposalId = param.ProposalId,
                BrandId = brand.Id,
                TypeId = type.Id,
                Status = 0
            };

            _repositoryManager.Contest.Create(contest);
            await _repositoryManager.SaveAsync();

            var createdContest = await _repositoryManager.Contest.GetCreatedContest(group_id, param.Title, param.StartRegistration, trackChanges: false);

            return Ok(new { contestId = createdContest.Id });
        }

        /// <summary>
        /// Join to contest after payment (Role: Manager, Member)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{contest_id}/join")]
        public async Task<IActionResult> JoinToContest(int contest_id)
        {
            var account_id = _userAccessor.getAccountId();

            _repositoryManager.JoinContest.Create(
                new JoinedToContest
                {
                    AccountId = account_id,
                    ContestId = contest_id
                });
            await _repositoryManager.SaveAsync();

            return Ok("Save change success");
        }

        /// <summary>
        /// Add prizes to contest
        /// </summary>
        /// <param name="contest_id">Contest id return after create contest success</param>
        /// <param name="prizes_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{contest_id}/prizes")]
        public async Task<IActionResult> AddPrizeToContest(int contest_id, List<int> prizes_id)
        {
            foreach (var prizeId in prizes_id)
            {
                _repositoryManager.PrizeContest.Create(new PrizeContest { ContestId = contest_id, PrizeId = prizeId });
            }
            await _repositoryManager.SaveAsync();

            return Ok("Saves change success");
        }
    }
}
