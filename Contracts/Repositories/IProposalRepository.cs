using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts.Repositories
{
    public interface IProposalRepository
    {
        void CreateProposal(Proposal proposal);
    }
}
