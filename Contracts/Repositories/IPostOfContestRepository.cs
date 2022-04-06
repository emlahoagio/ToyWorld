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
    }
}
