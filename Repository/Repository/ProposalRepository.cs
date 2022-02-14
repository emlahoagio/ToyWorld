using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Pagination<ProposalInList>> GetWaitingProposal(PagingParameters paging, bool trackChanges)
        {
            var proposals = await FindByCondition(x => x.IsWaiting == true, trackChanges)
                .Include(x => x.Account)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                .ToListAsync();

            var count = proposals.Count;

            var pagingList = proposals.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            var data = proposals.Select(x => new ProposalInList
            {
                Id = x.Id,
                BrandName = x.Brand == null ? null : x.Brand.Name,
                Description = x.ContestDescription,
                MaxRegister = x.MaxRegister,
                MinRegister = x.MinRegister,
                OwnerAvatar = x.Account.Avatar,
                OwnerId = x.Account.Id,
                OwnerName = x.Account.Name,
                Title = x.Title,
                TypeName = x.Type == null ? null : x.Type.Name
            });

            var result = new Pagination<ProposalInList>
            {
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
                Count = count,
                Data = data
            };

            return result;
        }
    }
}
