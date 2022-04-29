using Entities.DataTransferObject;
using Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IProposalRepository
    {
        Task<Pagination<ProposalInList>> GetListByManager(PagingParameters paging);
        Task<IEnumerable<ProposalInList>> GetListProposal(int accountId);
        void CreateProposal(CreateProposalModel model, int accountId);
        void DeleteProposal(int proposalId);
    }
}
