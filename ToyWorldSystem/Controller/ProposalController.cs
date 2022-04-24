using Contracts;
using Entities.ErrorModel;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetListProposalByManager([FromQuery] PagingParameters paging)
        {
            var proposals = await _repository.Proposal.GetListByManager(paging);
            if (proposals == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No proposal exist");
            return Ok(proposals);
        }

        [HttpGet]
        [Route("{account_id}")]
        public async Task<IActionResult> GetListProposalByMember(int account_id)
        {
            var proposals = await _repository.Proposal.GetListProposal(account_id);
            return Ok(proposals);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProposal(CreateProposalModel model)
        {
            _repository.Proposal.CreateProposal(model, _userAccessor.GetAccountId());
            await _repository.SaveAsync();
            return Ok("Create Success!");
        }

        [HttpDelete]
        [Route("{proposalId}")]
        public async Task<IActionResult> DeleteProposal(int proposalId)
        {
            _repository.Proposal.DeleteProposal(proposalId);
            await _repository.SaveAsync();
            return Ok("Delete Success!");
        }
    }
}
