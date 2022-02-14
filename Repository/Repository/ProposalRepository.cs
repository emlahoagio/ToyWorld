using Contracts.Repositories;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ProposalRepository : RepositoryBase<Proposal>, IProposalRepository
    {
        public ProposalRepository(DataContext context) : base(context)
        {
        }

        public void CreateProposal(Proposal proposal) => Create(proposal);

        public async Task<Proposal> GetProposalToAddPrize(int proposal_id, bool trachChanges)
        {
            var proposal = await FindByCondition(x => x.Id == proposal_id, trachChanges)
                .Include(x => x.ProposalPrizes)
                .FirstOrDefaultAsync();

            if (proposal == null) return null;

            return proposal;
        }
    }
}
