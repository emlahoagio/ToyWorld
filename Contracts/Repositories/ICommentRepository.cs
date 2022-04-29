using Entities.DataTransferObject;
using Entities.Models;
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
        Task<int> GetNumOfCommentByPostId(int post_id, bool trackChanges);
        Task<CommentInPostDetail> GetCommentDetailOfPost(int account_id, int post_id, bool trackChanges);
        Task<int> GetNumOfCommentByTradingId(int post_id, bool trackChanges);
        Task<CommentInPostDetail> GetCommentDetailOfTradingPost(int accountId, int post_id, bool trackChanges);
        Task<Pagination<PostInList>> GetNumOfCommentForPostList(Pagination<PostInList> result_no_comment, bool trackChanges);
        Task<PostDetail> GetPostComment(PostDetail result_no_comment, bool trackChanges, int account_id);
        Task<TradingPostDetail> GetTradingComment(TradingPostDetail trading_post_detail_no_comment, int account_id, bool trackChanges);
        Task<Pagination<TradingPostInList>> GetNumOfCommentForTradingPostList(Pagination<TradingPostInList> result_no_comment, bool trackChanges);
        Task<Pagination<TradingManaged>> GetNumOfCommentForTradingPostList(Pagination<TradingManaged> result_no_comment, bool trackChanges);
    }
}
