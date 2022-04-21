using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IRewardRepository
    {
        void Create(Reward reward);
        Task<List<RewardReturn>> GetContestReward(int contest_id, bool trackChanges);
        Task Delete(int contestId, bool trackChanges);
        Task Delete(int account_id, int contest_id, bool trackChanges);
        Task<Pagination<ContestManaged>> CheckRewardOfContest(Pagination<ContestManaged> contests, bool trackChanges);
        Task<List<int>> GetIdOfPostHasReward(int contest_id, bool trackChanges);
        Task<List<int>> GetPrizeInContestHasReward(int contest_id, bool trackChanges);
    }
}
