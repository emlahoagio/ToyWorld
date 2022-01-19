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

        public CommentController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
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
            _repositoryManager.Comment.CreateComment(
                new Entities.Models.Comment
                {
                    AccountId = param.AccountId,
                    Content = param.Content,
                    PostId = param.PostId
                });

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
    }
}
