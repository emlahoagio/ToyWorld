using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPrizeContestRepository
    {
        Task<Pagination<ContestInGroup>> GetPrizeForContest(Pagination<ContestInGroup> param);
        Task<List<PrizeReturn>> GetPrizeForContestDetail(int contest_id, bool trackChanges);
        void Create(PrizeContest prizeContest);
    }
}
