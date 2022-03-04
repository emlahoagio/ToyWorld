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
        /// Get list account (Role: Admin)
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListAccount([FromQuery] PagingParameters paging)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            if (current_account.Role != 0) throw new ErrorDetails(HttpStatusCode.BadRequest, "Not enough role to get");

            var result = await _repository.Account.GetListAccount(paging, trackChanges: false);

            if (result == null) throw new ErrorDetails(HttpStatusCode.NotFound, "No more records in this page");

            return Ok(result);
        }

        /// <summary>
        /// Get account detail, not include post of account (Role: Manager, Member)
        /// </summary>
        /// <param name="account_id">Id of account want to get detail</param>
        /// <returns></returns>
        [HttpGet]
        [Route("detail/{account_id}")]
        public async Task<IActionResult> GetAccountDetail(int account_id)
        {
            var current_account_id = _userAccessor.getAccountId();

            var account = await _repository.Account.GetAccountDetail(account_id, current_account_id, trackChanges: false);

            if (account == null) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid account");

            return Ok(account);
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
        /// Get profile (Role: Manager, Member, Admin)
        /// </summary>
        /// <param name="account_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{account_id}/profile")]
        public async Task<IActionResult> GetProfile(int account_id)
        {
            var profile = await _repository.Account.GetProfile(account_id, trackChanges: false);

            if (profile == null) throw new ErrorDetails(HttpStatusCode.NotFound, "No account match with id");

            return Ok(profile);
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

            if (!account.Status) throw new ErrorDetails(HttpStatusCode.Unauthorized, "Account is disbled");

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

            if(current_login_account.Id == visit_account_id) throw new ErrorDetails(HttpStatusCode.BadRequest, "Can't follow yourself");

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

        /// <summary>
        /// Disable or Enable account (Role: Admin)
        /// </summary>
        /// <param name="account_id">Account id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("enable_disable/{account_id}")]
        public async Task<IActionResult> DisableEnableAccount(int account_id)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            if (current_account.Role != 0) throw new ErrorDetails(HttpStatusCode.BadRequest, "Not enough role to update");

            var update_account = await _repository.Account.GetAccountById(account_id, trackChanges: false);

            if (update_account.Status)
            {
                _repository.Account.DisableAccount(update_account);
            }else
            {
                _repository.Account.EnableAccount(update_account);
            }
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Update account role from member to manager (Role: Admin)
        /// </summary>
        /// <param name="account_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{account_id}/role/manager")]
        public async Task<IActionResult> UpdateAccountToManager(int account_id)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            if (current_account.Role != 0) throw new ErrorDetails(HttpStatusCode.BadRequest, "Not enough role to update");

            var update_account = await _repository.Account.GetAccountById(account_id, trackChanges: false);

            if (update_account.Role == 0) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid request");

            if (update_account.Role == 1) return Ok("Already manager");

            _repository.Account.UpdateAccountToManager(update_account);

            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Update account role from manager to member (Role: Admin)
        /// </summary>
        /// <param name="account_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{account_id}/role/member")]
        public async Task<IActionResult> UpdateAccountToMember(int account_id)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            if (current_account.Role != 0) throw new ErrorDetails(HttpStatusCode.BadRequest, "Not enough role to update");

            var update_account = await _repository.Account.GetAccountById(account_id, trackChanges: false);

            if (update_account.Role == 0) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid request");

            if (update_account.Role == 2) return Ok("Already member");

            _repository.Account.UpdateAccountToMember(update_account);

            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Update profile (Role: Member, Manager)
        /// </summary>
        /// <param name="account_id"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{account_id}/profile")]
        public async Task<IActionResult> UpdateProfile(int account_id, UpdateAccountParameters param)
        {
            var curent_account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(),trackChanges: false);

            if (account_id != curent_account.Id) throw new ErrorDetails(HttpStatusCode.BadRequest, "Can't update another user profile");

            curent_account.Name = param.Name;
            curent_account.Email = param.Email;
            curent_account.Phone = param.Phone;
            curent_account.Avatar = param.Avatar;
            curent_account.Biography = param.Biography;
            curent_account.Gender = param.Gender;

            _repository.Account.Update(curent_account);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
    }
}
