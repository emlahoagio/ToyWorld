using Contracts;
using Entities.ErrorModel;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUserAccessor _userAccessor;

        public CommentController(IRepositoryManager repositoryManager, IUserAccessor userAccessor)
        {
            _repositoryManager = repositoryManager;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Create new comment in post
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("news/post")]
        public async Task<IActionResult> CreateCommentInPost(NewCommentParameter param)
        {
            var accountId = _userAccessor.getAccountId();

            var comment = new Entities.Models.Comment
            {
                AccountId = accountId,
                Content = param.Content,
                PostId = param.PostId,
                TradingPostId = null
            };

            _repositoryManager.Comment.CreateComment(comment);
            var account = _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), false).Result;
            var noti = new CreateNotificationModel
            {
                PostId = param.PostId,
                Content = account.Name + " has commented on your post",
                AccountId = 0,
            };
            _repositoryManager.Notification.CreateNotification(noti);

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Create new comment in trading post
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("news/trading_post")]
        public async Task<IActionResult> CreateCommentInTradingPost(NewCommentParameter param)
        {
            var accountId = _userAccessor.getAccountId();

            var comment = new Entities.Models.Comment
            {
                AccountId = accountId,
                Content = param.Content,
                PostId = null,
                TradingPostId = param.PostId
            };

            _repositoryManager.Comment.CreateComment(comment);

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// React Comment
        /// </summary>
        /// <param name="comment_id">Id of post return in post detail</param>
        /// <returns></returns>
        [HttpPut]
        [Route("reacts/{comment_id}")]
        public async Task<IActionResult> ReactComment(int comment_id)
        {
            var comment = await _repositoryManager.Comment.GetCommentReactById(comment_id, trackChanges: false);

            var accountId = _userAccessor.getAccountId();

            if (comment == null)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "No comment matches with post id");

            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);

            if (account == null)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "No account matches with account id");

            //account is reacted
            var isReacted = _repositoryManager.Comment.IsReactedComment(comment, accountId);

            if (isReacted)
            {
                //un react
                _repositoryManager.ReactComment.DeleteReact(
                    new Entities.Models.ReactComment { AccountId = accountId, CommentId = comment_id });
            }
            else
            {
                //react
                _repositoryManager.ReactComment.CreateReact(
                    new Entities.Models.ReactComment { AccountId = accountId, CommentId = comment_id });
            }

            await _repositoryManager.SaveAsync();

            return Ok(new { message = "Save changes success" });
        }

        /// <summary>
        /// Update comment (Role: Member, Manager)
        /// </summary>
        /// <param name="request_comment"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentParameters request_comment)
        {
            var account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var comment = await _repositoryManager.Comment.GetUpdateCommentById(request_comment.Id, trackChanges: false);

            if (comment.AccountId != account.Id)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "You're not owner to update");

            _repositoryManager.Comment.UpdateComment(comment, request_comment.Content);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Delete comment by id
        /// </summary>
        /// <param name="comment_id">Comment return in post detail</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{comment_id}")]
        public async Task<IActionResult> DeleteComment(int comment_id)
        {
            var account_id = _userAccessor.getAccountId();

            var comment = await _repositoryManager.Comment.GetUpdateCommentById(comment_id, trackChanges: false);

            if(comment == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid comment");

            if (comment.AccountId != account_id && comment.Post.AccountId != account_id)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "You're not owner to remove");

            _repositoryManager.Comment.DeleteComment(comment);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
    }
}
