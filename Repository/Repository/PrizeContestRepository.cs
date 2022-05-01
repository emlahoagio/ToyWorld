using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class PrizeContestRepository : RepositoryBase<PrizeContest>,IPrizeContestRepository
    {
        public PrizeContestRepository(DataContext context) : base(context)
        {
        }

        public async Task Delete(int contest_id, bool trackChanges)
        {
            var prizeContests = await FindByCondition(x => x.ContestId == contest_id, trackChanges).ToListAsync();
            
            foreach(var prize in prizeContests)
            {
                Delete(prize);
            }
        }

        public async Task<List<PrizeReturn>> GetPrizeForContestDetail(int contest_id, bool trackChanges)
        {
            var prize_list = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .Include(x => x.Prize)
                .ToListAsync();

            var result = prize_list.Select(x => new PrizeReturn
            {
                Description = x.Prize.Description,
                Name = x.Prize.Name,
                Id = x.Prize.Id,
                Value = double.Parse(x.Prize.Value),
            }).ToList();

            return result;
        }

        public async Task<Pagination<PrizeOfContest>> GetPrizeForEndContest(List<int> prizeHasReward, int contest_id, bool trackChanges)
        {
            var prizesContest = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .Include(x => x.Prize)
                .ToListAsync();

            var result = prizesContest.Where(x => !prizeHasReward.Contains(x.PrizeId)).Select(x => new PrizeOfContest
            {
                Id = x.Prize.Id,
                Value = x.Prize.Value,
                Description = x.Prize.Description,
                Name = x.Prize.Name
            }).OrderByDescending(y => y.Value).ToList();

            return new Pagination<PrizeOfContest>
            {
                Count = result.Count,
                Data = result,
                PageNumber = 1,
                PageSize = result.Count
            };
        }

        public async Task<Pagination<ContestInGroup>> GetPrizeForContest(Pagination<ContestInGroup> param)
        {
            var contests = param.Data;

            var dataResult = new List<ContestInGroup>();

            foreach (var contest in contests)
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
                }).OrderByDescending(x => x.Value).ToList();
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

        public async Task<Pagination<ContestManaged>> GetPrizeForContest(Pagination<ContestManaged> param)
        {
            var contests = param.Data;

            var dataResult = new List<ContestManaged>();

            foreach (var contest in contests)
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
                }).OrderByDescending(x => x.Value).ToList();
                dataResult.Add(contest);
            }

            var result = new Pagination<ContestManaged>
            {
                Count = param.Count,
                Data = dataResult,
                PageNumber = param.PageNumber,
                PageSize = param.PageSize
            };

            return result;
        }
    }
}
