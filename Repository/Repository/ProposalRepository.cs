using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ProposalRepository : RepositoryBase<Proposal>, IProposalRepository
    {
        public ProposalRepository(DataContext context) : base(context)
        {
        }
        public void CreateProposal(CreateProposalModel model, int accountId)
        {
            var proposal = new Proposal
            {
                GroupId = model.GroupId,
                Description = model.Description,
                Reason = model.Reason,
                Rule = model.Rule,
                Title = model.Title,
                Slogan = model.Slogan,
                AccountId = accountId
            };
            Create(proposal);
        }

        public void DeleteProposal(int proposalId)
        {
            var proposal = FindByCondition(x => x.Id == proposalId, true).FirstOrDefaultAsync().Result;
            Delete(proposal);
        }

        public async Task<Pagination<Proposal>> GetListByManager(PagingParameters paging)
        {
            var proposals = await FindAll(false).ToListAsync();
            var subProposals = proposals.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);
            var result = new Pagination<Proposal>
            {
                Data = subProposals,
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
            };
            return result;
        }

        public async Task<IEnumerable<Proposal>> GetListProposal(int accountId)
        {
            var proposal = await FindByCondition(x => x.AccountId == accountId, false).ToListAsync();
            return proposal;
        }
    }
}
