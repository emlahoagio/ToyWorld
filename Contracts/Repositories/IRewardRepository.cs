using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IRewardRepository
    {
        Task<List<RewardReturn>> GetContestReward(int contest_id, bool trackChanges);
    }
}
