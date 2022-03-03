﻿using Contracts.Repositories;
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
    public class RewardRepository : RepositoryBase<Reward>, IRewardRepository
    {
        public RewardRepository(DataContext context) : base(context)
        {
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
                    SumOfStart = x.PostOfContest.Rates.Select(y => y.NumOfStart).ToList().Sum()
                }
            }).ToList();

            return result;
        }
    }
}