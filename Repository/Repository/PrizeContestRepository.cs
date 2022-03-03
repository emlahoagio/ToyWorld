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
    public class PrizeContestRepository : RepositoryBase<PrizeContest>,IPrizeContestRepository
    {
        public PrizeContestRepository(DataContext context) : base(context)
        {
        }

        public async Task<Pagination<ContestInGroup>> GetPrizeForContest(Pagination<ContestInGroup> param)
        {
            var contests = param.Data;

            var dataResult = new List<ContestInGroup>();

            foreach(var contest in contests)
            {
                var prizes = await FindByCondition(x => x.ContestId == contest.Id, false).Include(x => x.Prize).ToListAsync();
                contest.Prizes = prizes.Select(x => new PrizeOfContest
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
                dataResult.Add(contest);
            }

            var result = new Pagination<ContestInGroup>
            {
                Count = param.Count,
                Data = dataResult,
                PageNumber = param.PageNumber,
                PageSize = param.PageSize
            };

            return result;
        }

        public async Task<List<PrizeReturn>> GetPrizeForContestDetail(int contest_id, bool trackChanges)
        {
            var prize_list = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .Include(x => x.Prize).ThenInclude(x => x.Images)
                .ToListAsync();

            var result = prize_list.Select(x => new PrizeReturn
            {
                Description = x.Prize.Description,
                Id = x.Prize.Id,
                Images = x.Prize.Images.Select(y => new ImageReturn { Id = y.Id, Url = y.Url }).ToList(),
                Name = x.Prize.Name,
                Value = x.Prize.Value
            }).ToList();

            return result;
        }
    }
}
