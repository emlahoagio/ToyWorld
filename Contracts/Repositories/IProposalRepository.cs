using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IProposalRepository
    {
        void CreateProposal(Proposal proposal);
        Task<Proposal> GetProposalToAddPrize(int proposal_id, bool trachChanges);
        Task<Pagination<ProposalInList>>GetWaitingProposal(PagingParameters paging, bool trackChanges);
    }
}
