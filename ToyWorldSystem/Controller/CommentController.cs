using Contracts;
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
    }
}
