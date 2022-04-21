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

        public async Task<Pagination<ProposalInList>> GetListByManager(PagingParameters paging)
        {
            var proposals = await FindAll(false)
                .OrderByDescending(x => x.Id)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Include(x => x.Account)
                .ToListAsync();

            if (proposals.Count == 0) return null;

            var result = new Pagination<ProposalInList>
            {
                Data = proposals.Select(x => new ProposalInList 
                {
                    Description = x.Description,
                    GroupId = x.GroupId,
                    Id = x.Id,
                    OwnerId = x.Account.Id,
                    OwnerName = x.Account.Name,
                    Reason = x.Reason,
                    Rule = x.Rule,
                    Slogan = x.Slogan,
                    Title = x.Title
                }).ToList(),
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
            };
            return result;
        }

        public async Task<IEnumerable<ProposalInList>> GetListProposal(int accountId)
        {
            var proposal = await FindByCondition(x => x.AccountId == accountId, false)
                .OrderByDescending(x => x.Id)
                .Include(x => x.Account)
                .ToListAsync();

            return proposal.Select(x => new ProposalInList
            {
                Description = x.Description,
                GroupId = x.GroupId,
                Id = x.Id,
                OwnerId = x.Account.Id,
                OwnerName = x.Account.Name,
                Reason = x.Reason,
                Rule = x.Rule,
                Slogan = x.Slogan,
                Title = x.Title
            }).ToList();
        }
    }
}
