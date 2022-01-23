using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ICommentRepository
    {
        void CreateComment(Comment comment);
        Task<Comment> GetCommentReactById(int comment_id, bool trackChanges);
        bool IsReactedComment(Comment comment, int accountId);
        Task<Comment> GetUpdateCommentById(int comment_id, bool trackChanges);
        void UpdateComment(Comment comment, string content);
        void DeleteComment(Comment comment);
    }
}
