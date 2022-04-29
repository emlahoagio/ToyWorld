using Entities.DataTransferObject;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IReactCommentRepository
    {
        void CreateReact(ReactComment reactComment);
        void DeleteReact(ReactComment reactComment);
        Task<List<AccountReact>> GetAccountReactComment(int comment_id, bool trackChanges);
        Task<int> GetNumOfReact(int comment_id, bool trackChanges);
        Task DeleteReact(int comment_id, bool trackChanges);
    }
}
