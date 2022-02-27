using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IJoinedContestRepository
    {
        Task<bool> IsJoinedToContest(int contest_id, int account_id, bool trackChanges);
        void Create(JoinedToContest join);
    }
}
