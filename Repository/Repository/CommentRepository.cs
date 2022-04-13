using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }
        public void CreateComment(Comment comment) => Create(comment);

        public void DeleteComment(Comment comment) => Delete(comment);

        public async Task<CommentInPostDetail> GetCommentDetailOfPost(int accountId, int post_id, bool trackChanges)
        {
            var comments = await FindByCondition(x => x.PostId == post_id, trackChanges)
                .OrderByDescending(x => x.CommentDate)
                .Include(x => x.Account)
                .Include(x => x.ReactComments)
                .ToListAsync();
            var count = await FindByCondition(x => x.PostId == post_id, trackChanges).CountAsync();

            var result = new CommentInPostDetail
            {
                Count = count,
                Comments = comments.Select(x => new CommentReturn
                {
                    CommentDate = x.CommentDate,
                    Content = x.Content,
                    Id = x.Id,
                    IsReacted = x.ReactComments.Where(y => y.AccountId == accountId).Count() != 0,
                    NumOfReact = x.ReactComments.Count,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerId = x.AccountId.Value,
                    OwnerName = x.Account.Name
                }).ToList()
            };

            return result;
        }

        public async Task<Comment> GetCommentReactById(int comment_id, bool trackChanges)
        {
            var comment = await FindByCondition(x => x.Id == comment_id, trackChanges)
                .Include(x => x.ReactComments)
                .FirstOrDefaultAsync();

            if (comment == null) return null;

            return comment;
        }

        public async Task<int> GetNumOfCommentByPostId(int post_id, bool trackChanges)
        {
            var count = await FindByCondition(x => x.PostId == post_id, trackChanges).CountAsync();

            return count;
        }

        public async Task<Pagination<TradingPostInList>> GetNumOfCommentForTradingPostList(Pagination<TradingPostInList> result_no_comment, bool trackChanges)
        {
            var result = new List<TradingPostInList>();

            foreach (var post in result_no_comment.Data)
            {
                var comments = await FindByCondition(x => x.TradingPostId == post.Id, trackChanges).ToListAsync();

                post.NoOfComment = comments.Count();

                result.Add(post);
            }

            result_no_comment.Data = result;
            return result_no_comment;
        }
        
        public async Task<Pagination<TradingManaged>> GetNumOfCommentForTradingPostList(Pagination<TradingManaged> result_no_comment, bool trackChanges)
        {
            var result = new List<TradingManaged>();

            foreach (var post in result_no_comment.Data)
            {
                var comments = await FindByCondition(x => x.TradingPostId == post.Id, trackChanges).ToListAsync();

                post.NoOfComment = comments.Count();

                result.Add(post);
            }

            result_no_comment.Data = result;
            return result_no_comment;
        }

        public async Task<TradingPostDetail> GetTradingComment(TradingPostDetail trading_post_detail_no_comment, int account_id ,bool trackChanges)
        {
            var comments = await FindByCondition(x => x.TradingPostId == trading_post_detail_no_comment.Id, trackChanges)
                .Include(x => x.Account)
                .Include(x => x.ReactComments)
                .ToListAsync();

            if (comments != null)
            {
                trading_post_detail_no_comment.Comment = comments.Select(x => new CommentReturn
                {
                    CommentDate = x.CommentDate,
                    Content = x.Content,
                    Id = x.Id,
                    NumOfReact = x.ReactComments.Count(),
                    OwnerAvatar = x.Account.Avatar,
                    OwnerId = x.AccountId.Value,
                    OwnerName = x.Account.Name,
                    IsReacted = x.ReactComments.Where(x => x.AccountId == account_id).Count() != 0
                }).ToList();
            }

            return trading_post_detail_no_comment;
        }

        public async Task<Comment> GetUpdateCommentById(int comment_id, bool trackChanges)
        {
            var comment = await FindByCondition(x => x.Id == comment_id, trackChanges)
                .Include(x => x.Post)
                .FirstOrDefaultAsync();

            if (comment == null) return null;

            return comment;
        }

        public bool IsReactedComment(Comment comment, int accountId)
        {
            bool result = false;
            foreach(var reactComment in comment.ReactComments)
            {
                if(reactComment.AccountId == accountId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void UpdateComment(Comment comment, string content)
        {
            comment.Content = content;
            Update(comment);
        }
    }
}
