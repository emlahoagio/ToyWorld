using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IJoinedContestRepository
    {
        Task<bool> IsJoinedToContest(int contest_id, int account_id, bool trackChanges);
        Task<Pagination<AccountInList>> GetListSubscribers(int contest_id, PagingParameters paging, bool trackChanges);
        Task<JoinedToContest> GetSubsCriberToDelete(int contest_id, int account_id, bool trackChanges);
        void Create(JoinedToContest join);
        void Delete(JoinedToContest join);
    }
}
