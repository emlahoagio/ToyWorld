using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPostOfContestRepository
    {
        void Create(PostOfContest postOfContest);
        Task<Pagination<PostOfContestInList>> GetPostOfContest(int contest_id, PagingParameters paging, int current_account_id, bool trackChanges);
        Task<List<PostOfContestToEndContest>> GetPostOfContestForEndContest(int contest_id, bool trackChanges);
        Task<int> GetOwnerByPostOfContestId(int id);
        Task<List<int>> GetPostOfContest(int contest_id, bool trackChanges);
        Task Delete(int contest_id, bool trackChanges);
        Task<List<int>> GetIdOfPost(int contest_id, bool trackChanges);
        Task<List<TopSubmission>> GetPostOfContestById(List<int> ids, bool trackchanges);
        Task<List<PostOfContest>> GetPostToDelete(int contest_id, int account_id, bool trackChanges);
        void Delete(PostOfContest post);
        Task<PostOfContest> GetPostOfContestById(int post_of_contest_id, bool trackchanges);
        Task<bool> IsReachPostLimit(int accountId, int contest_id, bool trackChanges);
    }
}
