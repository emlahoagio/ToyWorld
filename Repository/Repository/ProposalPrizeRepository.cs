using Contracts.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class ProposalPrizeRepository : RepositoryBase<ProposalPrize>, IProposalPrizeRepository
    {
        public ProposalPrizeRepository(DataContext context) : base(context)
        {
        }
    }
}
