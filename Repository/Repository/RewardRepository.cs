using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RewardRepository : RepositoryBase<Reward>, IRewardRepository
    {
        public RewardRepository(DataContext context) : base(context)
        {
        }

        public async Task<Pagination<ContestManaged>> CheckRewardOfContest(Pagination<ContestManaged> contests, bool trackChanges)
        {
            var data = new List<ContestManaged>();

            foreach(var contest in contests.Data)
            {
                contest.IsHasReward = await FindByCondition(x => x.ContestId == contest.Id, trackChanges).CountAsync() > 0;

                data.Add(contest);
            }
            contests.Data = data;

            return contests;
        }

        public async Task Delete(int contestId, bool trackChanges)
        {

            var rewards = await FindByCondition(x => x.ContestId == contestId, trackChanges).ToListAsync();

            foreach(var reward in rewards)
            {
                Delete(reward);
            }
        }

        public async Task Delete(int account_id, int contest_id, bool trackChanges)
        {
            var rewards = await FindByCondition(x => x.ContestId == contest_id && x.AccountId == account_id, trackChanges).ToListAsync();

            if(rewards.Count > 0)
            {
                foreach(var reward in rewards)
                {
                    Delete(reward);
                }
            }
        }

        public async Task<List<RewardReturn>> GetContestReward(int contest_id, bool trackChanges)
        {
            var rewards = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .Include(x => x.PostOfContest).ThenInclude(x => x.Rates)
                .Include(x => x.Account)
                .Include(x => x.Prize)
                .OrderByDescending(x => x.Prize.Value)
                .ToListAsync();

            var result = rewards.Select(x => new RewardReturn
            {
                Id = x.Id,
                Post = new PostOfContestInReward
                {
                    Content = x.PostOfContest.Content,
                    Id = x.PostOfContest.Id,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerName = x.Account.Name,
                    SumOfStart = x.PostOfContest.Rates.Select(y => y.NumOfStar).ToList().Sum()
                },
                Prizes = new PrizeReturn
                {
                    Description = x.Prize.Description,
                    Id = x.Prize.Id,
                    Name = x.Prize.Name,
                    Value = x.Prize.Value
                }
            }).ToList();

            return result;
        }

        public async Task<List<int>> GetIdOfPostHasReward(int contest_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .Select(x => x.PostOfContestId)
                .ToListAsync();

            return result;
        }

        public async Task<List<int>> GetPrizeInContestHasReward(int contest_id, bool trackChanges)
        {
            var prizeids = await FindByCondition(x => x.ContestId == contest_id, trackChanges).Select(x => x.PrizeId).ToListAsync();

            return prizeids;
        }
    }
}
