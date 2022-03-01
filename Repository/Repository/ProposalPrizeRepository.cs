using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ProposalPrizeRepository : RepositoryBase<ProposalPrize>, IProposalPrizeRepository
    {
        public ProposalPrizeRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<PrizeReturn>> GetPrizesOfProposal(int proposal_id, bool trackChanges)
        {
            var prizes = await FindByCondition(x => x.ProposalId == proposal_id, trackChanges)
                .Include(x => x.Prize).ThenInclude(x => x.Images)
                .ToListAsync();

            if (prizes.Count == 0) return null;

            var result = prizes.Select(x => new PrizeReturn
            {
                Id = x.Prize.Id,
                Description = x.Prize.Description,
                Images = x.Prize.Images.Select(y => new ImageReturn
                {
                    Id = y.Id,
                    Url = y.Url
                }).ToList(),
                Name = x.Prize.Name,
                Value = x.Prize.Value
            }).ToList();

            return result;
        }
    }
}
