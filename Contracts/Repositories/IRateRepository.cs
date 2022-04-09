using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IRateRepository
    {
        void Create(Rate rate);
        Task<bool> IsRated(int post_id, int account_id, bool trackChanges);
        Task<Pagination<PostOfContestInList>> GetRateForPostOfContest(Pagination<PostOfContestInList> post_no_rate, int account_id, bool trackChanges);
        Task Delete(List<int> listPostId, bool trackChanges);
        Task<List<int>> GetIdOfPostInTop(List<int> submissionsId, bool trackChanges);
        Task<List<TopSubmission>> GetRateForPostOfContest(List<TopSubmission> post, bool trackChanges);
    }
}
