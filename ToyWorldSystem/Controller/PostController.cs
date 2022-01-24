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
using System.Net;
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

        /// <summary>
        /// Get post by account Id
        /// </summary>
        /// <param name="account_id">Id of account return in login function</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("account/{account_id}")]
        public async Task<IActionResult> GetListPostByAccount(int account_id, [FromQuery] PagingParameters paging)
        {
            var result = await _repositoryManager.Post.GetPostByAccountId(account_id, trackChanges: false, paging);

            if (result == null) throw new ErrorDetails(HttpStatusCode.NotFound, "This account has no post yet");

            return Ok(result);
        }

        /// <summary>
        /// Get Waiting post (Role: Manager, Member)
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("waiting")]
        public async Task<IActionResult> GetWaitingPost([FromQuery]PagingParameters paging)
        {
            var accountId = _userAccessor.getAccountId();
            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);

            Pagination<WaitingPost> result;
            if(account.Role == 1)
            {
                result = await _repositoryManager.Post.GetWaitingPost(trackChanges: false, paging);
            }else
            {
                result = await _repositoryManager.Post.GetWaitingPost(trackChanges: false, paging, accountId);
            }

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
            var post = await _repositoryManager.Post.GetPostReactById(post_id, trackChanges: false);

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

        /// <summary>
        /// Approve post (Role: Manager)
        /// </summary>
        /// <param name="post_id">Id of post return in get list post</param>
        /// <returns></returns>
        [HttpPut]
        [Route("approve/{post_id}")]
        public async Task<IActionResult> ApprovePost(int post_id)
        {
            var accountId = _userAccessor.getAccountId();
            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);

            if (account.Role != 1) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid request");

            var post = await _repositoryManager.Post.GetPostApproveOrDenyById(post_id, trackChanges: false);
            if (post == null) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid post id");

            _repositoryManager.Post.ApprovePost(post);
            await _repositoryManager.SaveAsync();

            //Send notification

            return Ok("Save changes success");
        }

        /// <summary>
        /// Deny post (Role: Manager)
        /// </summary>
        /// <param name="post_id">Id of post return in get list post</param>
        /// <returns></returns>
        [HttpPut]
        [Route("deny/{post_id}")]
        public async Task<IActionResult> DenyPost(int post_id)
        {
            var accountId = _userAccessor.getAccountId();
            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);

            if (account.Role != 1) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid request");

            var post = await _repositoryManager.Post.GetPostApproveOrDenyById(post_id, trackChanges: false);
            if (post == null) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid post id");

            _repositoryManager.Post.DenyPost(post);
            await _repositoryManager.SaveAsync();

            //Send notification

            return Ok("Save changes success");
        }

        /// <summary>
        /// Disable post (Role: Manager, Member)
        /// </summary>
        /// <param name="post_id">Id of post return in get list, or get detail</param>
        /// <returns></returns>
        [HttpPut]
        [Route("disable/{post_id}")]
        public async Task<IActionResult> DisablePost(int post_id)
        {
            var accountId = _userAccessor.getAccountId();
            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);

            var post = await _repositoryManager.Post.GetDisablePost(post_id, trackChanges: false);
            if (post == null) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid post");

            //check not owner, not manager
            if(post.AccountId != accountId && account.Role != 1)
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid request");
            }

            _repositoryManager.Post.DisablePost(post);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
    }
}
