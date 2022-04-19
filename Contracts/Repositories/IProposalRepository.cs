using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IProposalRepository
    {
        Task<Pagination<Proposal>> GetListByManager(PagingParameters paging);
        Task<IEnumerable<Proposal>> GetListProposal(int accountId);
        void CreateProposal(CreateProposalModel model, int accountId);
        void DeleteProposal(int proposalId);
    }
}
