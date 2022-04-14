using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IEvaluateContestRepository
    {
        void Create(Evaluate evaluate);
        Task Delete(int contestId, bool trackChanges);
        Task<bool> IsEvaluated(int current_accountId, int contest_id, bool trackChanges);
    }
}
