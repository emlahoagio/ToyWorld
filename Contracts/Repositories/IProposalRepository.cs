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
        Task<Proposal> GetProposalToDenyOrApprove(int proposal_id, bool trackChanges);
        Task<ProposalCreateContest> GetInformationToCreateContest(int proposal_id, bool trackChanges);
        Task<Pagination<SendProposal>> GetSendProposal(PagingParameters paging, int account_id, bool trackChanges);
        void DenyProposal(Proposal proposal);
        void ApproveProposal(Proposal proposal);
    }
}
