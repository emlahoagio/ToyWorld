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
        [Route("new")]
        public async Task<IActionResult> CreateComment(NewCommentParameter param)
        {
            var accountId = _userAccessor.getAccountId();

            _repositoryManager.Comment.CreateComment(
                new Entities.Models.Comment
                {
                    AccountId = accountId,
                    Content = param.Content,
                    PostId = param.PostId
                });

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
        public async Task<IActionResult> ReactPost(int comment_id)
        {
            var comment = await _repositoryManager.Comment.GetCommentById(comment_id, trackChanges: false);

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
    }
}
