using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IReactCommentRepository
    {
        void CreateReact(ReactComment reactComment);
        void DeleteReact(ReactComment reactComment);
        Task<List<AccountReact>> GetAccountReactComment(int comment_id, bool trackChanges);
    }
}
