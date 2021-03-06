using Entities.DataTransferObject;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IJoinedContestRepository
    {
        Task<bool> IsJoinedToContest(int contest_id, int account_id, bool trackChanges);
        Task<List<AccountInList>> GetListSubscribers(int contest_id, bool trackChanges);
        Task<JoinedToContest> GetSubsCriberToDelete(int contest_id, int account_id, bool trackChanges);
        void Create(JoinedToContest join);
        void BandSubscribers(JoinedToContest join);
        Task Delete(int contest_id, bool trackChanges);
        Task<bool> IsBan(int contest_id, int account_id, bool trackChanges);
    }
}
