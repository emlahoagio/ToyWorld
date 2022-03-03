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
    }
}
