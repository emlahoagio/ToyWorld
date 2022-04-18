using Contracts;
using Entities.ErrorModel;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/[controller]")]
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
        [Route("proposals")]
        public async Task<IActionResult> GetListProposalByManager([FromQuery] PagingParameters paging)
        {
            var proposals = await _repository.Proposal.GetListByManager(paging);
            return Ok(proposals);
        }

        [HttpGet]
        [Route("{accountid}/proposals")]
        public async Task<IActionResult> GetListProposalByMember(int account_id)
        {
            var proposals = await _repository.Proposal.GetListProposal(account_id);
            return Ok(proposals);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProposal(CreateProposalModel model)
        {
            _repository.Proposal.CreateProposal(model, _userAccessor.getAccountId());
            await _repository.SaveAsync();
            return Ok("Create Success!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProposal(int proposalId)
        {
            _repository.Proposal.DeleteProposal(proposalId);
            await _repository.SaveAsync();
            return Ok("Delete Success!");
        }
    }
}
