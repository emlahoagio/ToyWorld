using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IContestRepository
    {
        Task<IEnumerable<HighlightContest>> GetHightlightContest(bool trackChanges);
        Task<Pagination<ContestInGroup>> GetContestInGroup(int group_id, int account_id, bool trackChanges, PagingParameters paging);
        Task<Contest> GetCreatedContest(int group_id, string title, DateTime? startRegistration, bool trackChanges);
        Task<ContestDetail> GetContestDetailInformation(int contest_id, bool trackChanges);
        void Create(Contest contest);
        Task StartRegistration(int contest_id, bool trackChanges);
        Task EndRegistration(int contest_id, bool trackChanges);
        Task StartContest(int contest_id, bool trackChanges);
        Task EndContest(int contestId, bool trackChanges);
        Task<bool> IsStartRegis (int contest_id, bool trackChanges);
        Task<bool> IsOpenContest(int contest_id, bool trackChanges);
        Task<Contest> GetEvaluateContest(int contest_id, bool trackChanges);
        Task<Pagination<ContestInGroup>> GetContestByGroups
            (int account_id, List<int> groups, PagingParameters paging, bool trackChanges);
        Task<Pagination<ContestManaged>> GetContestByStatus(int status, PagingParameters paging, bool trackChanges);
        Task Delete(int contest_id, bool trackChanges);
        Task<Pagination<ContestInGroup>> GetContestHighlightMb(int account_id, bool trackChanges, PagingParameters paging);
    }
}
