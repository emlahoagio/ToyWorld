using Entities.DataTransferObject;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPrizeContestRepository
    {
        Task<List<PrizeReturn>> GetPrizeForContestDetail(int contest_id, bool trackChanges);
        Task<Pagination<PrizeOfContest>> GetPrizeForEndContest(List<int> prizeHasReward, int contest_id, bool trackChanges);
        void Create(PrizeContest prizeContest);
        Task Delete(int contest_id, bool trackChanges);
        Task<Pagination<ContestInGroup>> GetPrizeForContest(Pagination<ContestInGroup> param);
        Task<Pagination<ContestManaged>> GetPrizeForContest(Pagination<ContestManaged> contests);
    }
}
