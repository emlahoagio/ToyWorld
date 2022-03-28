using Entities.DataTransferObject;
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
        Task<TradingPostDetail> GetTradingComment(TradingPostDetail trading_post_detail_no_comment, int account_id, bool trackChanges);
        void UpdateComment(Comment comment, string content);
        void DeleteComment(Comment comment);
        Task<PostDetail> GetPostComment(PostDetail result_no_comment, bool trackChanges, int account_id);
        Task<Pagination<PostInList>> GetNumOfCommentForPostList(Pagination<PostInList> result_no_comment, bool trackChanges);
        Task<Pagination<TradingPostInList>> GetNumOfCommentForTradingPostList(Pagination<TradingPostInList> result_no_comment, bool trackChanges);
    }
}
