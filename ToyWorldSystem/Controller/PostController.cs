using Contracts;
using Entities.DataTransferObject;
using Entities.ErrorModel;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUserAccessor _userAccessor;

        public PostController(IRepositoryManager repositoryManager, IUserAccessor userAccessor)
        {
            _repositoryManager = repositoryManager;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Get list post by group id (Role: Members, Manager)
        /// </summary>
        /// <param name="group_id">Id return in get list</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id}")]
        public async Task<IActionResult> GetListPostByGroup(int group_id, [FromQuery]PagingParameters paging)
        {
            var result = await _repositoryManager.Post.GetPostByGroupId(group_id, trackChanges: false, paging);

            if(result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more posts in this group");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("waiting")]
        public async Task<IActionResult> GetWaitingPost([FromQuery]PagingParameters paging)
        {
            var result = await _repositoryManager.Post.GetWaitingPost(trackChanges: false, paging);

            if (result == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No waiting post");

            return Ok(result);
        }

        /// <summary>
        /// Get post detail (Role: Member, Manager)
        /// </summary>
        /// <param name="post_id">Id of post return in get list post</param>
        /// <returns></returns>
        [HttpGet]
        [Route("details/{post_id}")]
        public async Task<IActionResult> GetPostDetail(int post_id)
        {
            var result = await _repositoryManager.Post.GetPostDetail(post_id, trackChanges: false);

            if (result == null) 
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No post matches with the id: " + post_id);

            return Ok(result);
        }

        /// <summary>
        /// Create new post (Role: Manager, Member)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> CreatePost(NewPostParameter param)
        {
            var accountId = _userAccessor.getAccountId();

            _repositoryManager.Post.CreatePost(param, accountId);

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// React Post
        /// </summary>
        /// <param name="post_id">Id of post return in detail, or get list</param>
        /// <returns></returns>
        [HttpPut]
        [Route("reacts/{post_id}")]
        public async Task<IActionResult> ReactPost(int post_id)
        {
            var post = await _repositoryManager.Post.GetPostById(post_id, trackChanges: false);

            var accountId = _userAccessor.getAccountId();

            if (post == null) 
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "No post matches with post id");

            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);

            if(account == null)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "No account matches with account id");

            //account is reacted
            var isReacted = _repositoryManager.Post.IsReactedPost(post, accountId);

            if (isReacted)
            {
                //un react
                _repositoryManager.ReactPost.DeleteReact(
                    new Entities.Models.ReactPost { AccountId = accountId, PostId = post_id });
            }else
            {
                //react
                _repositoryManager.ReactPost.CreateReact(
                    new Entities.Models.ReactPost { AccountId = accountId, PostId = post_id });
            }

            await _repositoryManager.SaveAsync();

            return Ok(new {message = "Save changes success"});
        }
    }
}
