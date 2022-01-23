using Contracts;
using Entities.ErrorModel;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IFirebaseSupport _firebaseSupport;
        private readonly IUserAccessor _userAccessor;

        public AccountController(IRepositoryManager repository, IFirebaseSupport firebaseSupport, IUserAccessor userAccessor)
        {
            _repository = repository;
            _firebaseSupport = firebaseSupport;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Get account react post
        /// </summary>
        /// <param name="post_id">Post Id return in GetPostDetail</param>
        /// <returns></returns>
        [HttpGet]
        [Route("react_post/{post_id}")]
        public async Task<IActionResult> GetAccountReactPost(int post_id)
        {
            var result = await _repository.ReactPost.GetAccountReactPost(post_id, trackChanges: false);

            if (result == null) throw new ErrorDetails(HttpStatusCode.NotFound, "No one react this post");

            return Ok(result);
        }

        /// <summary>
        /// Get account react comment
        /// </summary>
        /// <param name="comment_id">Id of comment return in get post detail</param>
        /// <returns></returns>
        [HttpGet]
        [Route("react_comment/{comment_id}")]
        public async Task<IActionResult> GetAccountReactComment(int comment_id)
        {
            var result = await _repository.ReactComment.GetAccountReactComment(comment_id, trackChanges: false);

            if(result == null) throw new ErrorDetails(HttpStatusCode.NotFound, "No one react this comment");

            return Ok(result);
        }

        /// <summary>
        /// Get following account
        /// </summary>
        /// <param name="account_id">Account need to get following</param>
        /// <returns></returns>
        [HttpGet]
        [Route("following/{account_id}")]
        public async Task<IActionResult> GetFollowingAccount(int account_id)
        {
            var account = await _repository.FollowAccount.GetAccountFollowing(account_id, trackChanges: false);

            if (account == null) throw new ErrorDetails(HttpStatusCode.NotFound, "No account following");

            return Ok(account);
        }

        /// <summary>
        /// Get follower account
        /// </summary>
        /// <param name="account_id">Account need to get follower</param>
        /// <returns></returns>
        [HttpGet]
        [Route("follower/{account_id}")]
        public async Task<IActionResult> GetFollowerAccount(int account_id)
        {
            var account = await _repository.FollowAccount.GetAccountFollower(account_id, trackChanges: false);

            if (account == null) throw new ErrorDetails(HttpStatusCode.NotFound, "No account following");

            return Ok(account);
        }

        /// <summary>
        /// Login by google mail (Role: ALL)
        /// </summary>
        /// <param name="firebaseToken">Token get from firebase</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login_by_email")]
        public async Task<IActionResult> LoginByEmail(string firebaseToken)
        {
            //init firebase
            _firebaseSupport.initFirebase();
            //get email
            var email = await _firebaseSupport.getEmailFromToken(firebaseToken);
            if(email.Contains("Get email from token error: "))
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, email);
            }
            var account = await _repository.Account.getAccountByEmail(email, trackChanges: false);
            if(account == null)
            {
                throw new ErrorDetails(HttpStatusCode.Unauthorized, "This account is not exist in our system");
            }
            if (!account.Status)
            {
                throw new ErrorDetails(HttpStatusCode.Unauthorized, "This account is disable" );
            }
            return Ok(account);
        }

        /// <summary>
        /// Login by email and password
        /// </summary>
        /// <param name="unverify_account"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login_by_system_account")]
        public async Task<IActionResult> LoginByAccountSystem(AccountSystemParameters unverify_account)
        {
            var account = await _repository.Account.getAccountByEmail(unverify_account.Email, unverify_account.Password, trackChanges: false);

            if (account == null) throw new ErrorDetails(HttpStatusCode.Unauthorized, "Invalid username/password!");

            return Ok(account);
        }

        /// <summary>
        /// Follow or unfollow the current visit account
        /// </summary>
        /// <param name="visit_account_id">The account id of another user</param>
        /// <returns></returns>
        [HttpPost]
        [Route("follow_or_unfollow/{visit_account_id}")]
        public async Task<IActionResult> FollowOrUnfollowAccount(int visit_account_id)
        {
            var current_login_account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            if(current_login_account.Id == visit_account_id) Ok("Save changes success");

            var current_follow = new Entities.Models.FollowAccount
            {
                AccountId = current_login_account.Id,
                AccountFollowId = visit_account_id
            };
            var follow_account = await _repository.FollowAccount.GetFollowAccount(current_follow, trackChanges: false);

            if(follow_account == null)
            {
                _repository.FollowAccount.CreateFollow(current_follow);
            }else
            {
                _repository.FollowAccount.DeleteFollow(current_follow);
            }

            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
    }
}
