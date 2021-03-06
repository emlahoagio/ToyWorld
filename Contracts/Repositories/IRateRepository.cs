using Entities.DataTransferObject;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IRateRepository
    {
        void Create(Rate rate);
        Task<bool> IsRated(int post_id, int account_id, bool trackChanges);
        Task<Pagination<PostOfContestInList>> GetRateForPostOfContest(Pagination<PostOfContestInList> post_no_rate, int account_id, bool trackChanges);
        Task Delete(List<int> listPostId, bool trackChanges);
        Task<List<int>> GetIdOfPostInTop3(List<int> submissionsId, bool trackChanges);
        Task<List<TopSubmission>> GetRateForPostOfContest(List<TopSubmission> post, bool trackChanges);
        Task DeleteRateOfPost(PostOfContest post, bool trackChanges);
        Task<List<int>> GetIdOfPostInTop10(List<int> submissionsId, List<int> idHasRewards, bool trackChanges);
    }
}
