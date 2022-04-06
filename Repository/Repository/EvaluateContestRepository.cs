﻿using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class EvaluateContestRepository : RepositoryBase<Evaluate>, IEvaluateContestRepository
    {
        public EvaluateContestRepository(DataContext context) : base(context)
        {
        }

        public async Task Delete(int contestId, bool trackChanges)
        {
            var evaluates = await FindByCondition(x => x.ContestId == contestId, trackChanges).ToListAsync();

            foreach(var evaluate in evaluates)
            {
                Delete(evaluate);
            }
        }
    }
}
