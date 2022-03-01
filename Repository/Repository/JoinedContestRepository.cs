using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JoinedContestRepository : RepositoryBase<JoinedToContest>, IJoinedContestRepository
    {
        public JoinedContestRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> IsJoinedToContest(int contest_id, int account_id, bool trackChanges)
        {
            var joinedContest = await FindByCondition(x => x.ContestId == contest_id && x.AccountId == account_id, trackChanges)
                .FirstOrDefaultAsync();

            return joinedContest == null ? false : true;
        }
    }
}
