using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IProposalPrizeRepository
    {
        void Create(ProposalPrize prizeProposal);
        Task<List<PrizeReturn>> GetPrizesOfProposal(int proposal_id, bool trackChanges);
    }
}
