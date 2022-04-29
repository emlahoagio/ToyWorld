using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> IsEvaluated(int current_accountId, int contest_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.AccountId == current_accountId && x.ContestId == contest_id, trackChanges)
                .FirstOrDefaultAsync() != null;

            return result;
        }
    }
}
