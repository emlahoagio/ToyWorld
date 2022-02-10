using Contracts.Repositories;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class ProposalRepository : RepositoryBase<Proposal>, IProposalRepository
    {
        public ProposalRepository(DataContext context) : base(context)
        {
        }

        public void CreateProposal(Proposal proposal) => Create(proposal);
    }
}
