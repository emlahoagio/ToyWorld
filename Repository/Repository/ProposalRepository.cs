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

        public void ApproveProposal(Proposal proposal)
        {
            proposal.IsWaiting = false;
            proposal.Status = true;
            Update(proposal);
        }

        public void CreateProposal(Proposal proposal) => Create(proposal);

        public void DenyProposal(Proposal proposal)
        {
            proposal.IsWaiting = false;
            proposal.Status = false;
            Update(proposal);
        }

        public async Task<ProposalCreateContest> GetInformationToCreateContest(int proposal_id, bool trackChanges)
        {
            var proposal = await FindByCondition(x => x.Id == proposal_id, trackChanges)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                .FirstOrDefaultAsync();

            if (proposal == null) return null;

            var result = new ProposalCreateContest
            {
                BrandName = proposal.Brand.Name,
                Description = proposal.ContestDescription,
                Duration = proposal.Duration,
                Location = proposal.Location,
                MaxRegistration = proposal.MaxRegister,
                MinRegistration = proposal.MinRegister,
                Title = proposal.Title,
                TypeName = proposal.Type.Name
            };

            return result;
        }

        public async Task<Proposal> GetProposalToAddPrize(int proposal_id, bool trachChanges)
        {
            var proposal = await FindByCondition(x => x.Id == proposal_id, trachChanges)
                .Include(x => x.ProposalPrizes)
                .FirstOrDefaultAsync();

            if (proposal == null) return null;

            return proposal;
        }

        public async Task<Proposal> GetProposalToDenyOrApprove(int proposal_id, bool trackChanges)
        {
            var proposal = await FindByCondition(x => x.Id == proposal_id, trackChanges).FirstOrDefaultAsync();

            return proposal;
        }

        public async Task<Pagination<SendProposal>> GetSendProposal(PagingParameters paging, int account_id, bool trackChanges)
        {
            var proposals = await FindByCondition(x => x.AccountId == account_id, trackChanges)
                .Include(x => x.Account)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();

            var count = proposals.Count;

            var take_items = proposals.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            var list_result = take_items.Select(x => new SendProposal
            {
                BrandName = x.Brand == null ? "Unknow Brand" : x.Brand.Name,
                ContestDescription = x.ContestDescription,
                Duration = x.Duration,
                Id = x.Id,
                Images = x.Images.Select(y => new ImageReturn
                {
                    Id = y.Id,
                    Url = y.Url
                }).ToList(),
                Location = x.Location,
                MaxRegister = x.MaxRegister,
                MinRegister = x.MinRegister,
                Status = x.IsWaiting.Value ? "Waiting" : x.Status.Value ? "Accepted" : "Denied",
                Title = x.Title,
                TypeName = x.Type == null ? "Unknow Type" : x.Type.Name
            }).ToList();

            var result = new Pagination<SendProposal>
            {
                Count = count,
                Data = list_result,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
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
                Location = x.Location,
                Duration = x.Duration,
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
