using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IProposalPrizeRepository
    {
        void Create(ProposalPrize prizeProposal);
    }
}
