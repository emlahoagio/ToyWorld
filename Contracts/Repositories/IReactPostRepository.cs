using Entities.DataTransferObject;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IReactPostRepository
    {
        void CreateReact(ReactPost reactPost);
        void DeleteReact(ReactPost reactPost);
        Task<List<AccountReact>> GetAccountReactPost(int post_id, bool trackChanges);
        Task<Pagination<PostInList>> GetReactForPost(Pagination<PostInList> result, int account_id, bool trackChanges);
    }
}
