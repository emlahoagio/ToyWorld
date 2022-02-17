using Contracts;
using Entities.ErrorModel;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/proposals")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public ProposalController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Create new proposal (don't have prize)
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProposal(NewProposalParameters parameters)
        {
            var accountId = _userAccessor.getAccountId();

            var brand = _repository.Brand.GetBrandByName(parameters.BrandName, trackChanges: false);

            var type = _repository.Type.GetTypeByName(parameters.TypeName, trackChanges: false);

            var proposal = new Proposal
            {
                Title = parameters.Title,
                MinRegister = parameters.MinRegister,
                MaxRegister = parameters.MaxRegister,
                ContestDescription = parameters.Description,
                IsWaiting = true,
                AccountId = accountId,
                TypeId = type.Id,
                BrandId = brand.Id,
                TakePlace = parameters.TakePlace,
                Images = parameters.ImagesUrl
                .Select(x =>
                new Entities.Models.Image
                {
                    Url = x
                }).ToList()
            };

            _repository.Proposal.CreateProposal(proposal);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Select Prize for proposal after create
        /// </summary>
        /// <param name="proposal_id"></param>
        /// <param name="prizes_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{proposal_id}/prizes")]
        public async Task<IActionResult> AddProposalPrize(int proposal_id, [FromBody]List<int> prizes_id)
        {
            var proposal = await _repository.Proposal.GetProposalToAddPrize(proposal_id, trachChanges: false);

            var account_id = _userAccessor.getAccountId();

            if (proposal.AccountId != account_id) 
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "This proposal isn't belong to you");

            foreach(var prize_id in prizes_id)
            {
                _repository.ProposalPrize.Create(new ProposalPrize
                {
                    PrizeId = prize_id,
                    ProposalId = proposal.Id
                });
            }
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Get proposal is waiting to approve or deny
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("waiting")]
        public async Task<IActionResult> GetWaitingProposal([FromQuery] PagingParameters paging)
        {
            var proposals = await _repository.Proposal.GetWaitingProposal(paging, trackChanges: false);

            if (proposals == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more waiting proposal");

            return (Ok(proposals));
        }

        [HttpPut]
        [Route("deny/{proposal_id}")]
        public async Task<IActionResult> DenyProposal(int proposal_id)
        {
            var account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            if (account.Role != 1) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "You're not allow to deny");

            var proposal = await _repository.Proposal.GetProposalToDeny(proposal_id, trackChanges: false);

            if (proposal == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid Proposal");

            _repository.Proposal.DenyProposal(proposal);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
    }
}
