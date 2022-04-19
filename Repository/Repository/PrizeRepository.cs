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
    public class PrizeRepository : RepositoryBase<Prize>, IPrizeRepository
    {
        public PrizeRepository(DataContext context) : base(context)
        {
        }

        public void CreatePrize(Prize prize) => Create(prize);

        public async Task<Pagination<PrizeOfContest>> GetPrize(PagingParameters paging, bool trackChanges)
        {
            var prizes = await FindByCondition(x => x.IsDisabled == false, trackChanges).ToListAsync();

            var count = prizes.Count;

            var result = new Pagination<PrizeOfContest>
            {
                Count = count,
                Data = prizes
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Select(x => new PrizeOfContest
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Value = x.Value
                }).ToList(),
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }

        public void UpdatePrize(Prize prize) => Update(prize);

        public async Task UpdatePrize(EditPrizeParameters param, int prize_id, bool trackChanges)
        {
            var update_prize = await FindByCondition(x => x.Id == prize_id, trackChanges).FirstOrDefaultAsync();

            update_prize.Name = param.Name;
            update_prize.Description = param.Description;
            update_prize.Value = param.Value.ToString();

            Update(update_prize);
        }

        public async Task<PrizeReturn> GetUpdatePrize(int prize_id, bool trackChanges)
        {
            var prize = await FindByCondition(x => x.Id == prize_id, trackChanges)
                .FirstOrDefaultAsync();

            if (prize == null) return null;

            var result = new PrizeReturn
            {
                Description = prize.Description,
                Id = prize.Id,
                Name = prize.Name,
                Value = prize.Value
            };

            return result;
        }

        public async Task DisablePrize(int prize_id, bool trackChanges)
        {
            var prize = await FindByCondition(x => x.Id == prize_id, trackChanges).FirstOrDefaultAsync();

            prize.IsDisabled = true;
            Update(prize);
        }

        public async Task<Pagination<PrizeOfContest>> GetPrizeForEnd(PagingParameters paging, bool trackChanges)
        {
            var prizes = await FindByCondition(x => x.IsDisabled == false && x.Rewards != null, trackChanges)
                .Include(x => x.Rewards)
                .ToListAsync();

            var count = prizes.Count;

            var result = new Pagination<PrizeOfContest>
            {
                Count = count,
                Data = prizes
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Select(x => new PrizeOfContest
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Value = x.Value
                }).ToList(),
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }
    }
}
