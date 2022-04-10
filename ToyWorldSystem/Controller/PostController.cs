using Contracts;
using Entities.DataTransferObject;
using Entities.ErrorModel;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> GetListPostByGroup(int group_id, [FromQuery] PagingParameters paging)
        {
            var account_id = _userAccessor.getAccountId();

            var result_no_image_no_comment = await _repositoryManager.Post.GetPostByGroupId(group_id, trackChanges: false, paging, account_id);
            
            if (result_no_image_no_comment == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more posts in this group");
            }

            var result_no_comment = await _repositoryManager.Image.GetImageForListPost(result_no_image_no_comment, trackChanges: false);

            var result = await _repositoryManager.Comment.GetNumOfCommentForPostList(result_no_comment, trackChanges: false);

            return Ok(result);
        }

        /// <summary>
        /// Get post by account Id (Role: Manager, Member)
        /// </summary>
        /// <param name="account_id">Id of account return in login function</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("account/{account_id}")]
        public async Task<IActionResult> GetListPostByAccount(int account_id, [FromQuery] PagingParameters paging)
        {
            var result_no_image_no_comment = await _repositoryManager.Post.GetPostByAccountId(account_id, trackChanges: false, paging);

            if (result_no_image_no_comment == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "This account has no post yet");
            }

            var result_no_comment = await _repositoryManager.Image.GetImageForListPost(result_no_image_no_comment, trackChanges: false);

            var result = await _repositoryManager.Comment.GetNumOfCommentForPostList(result_no_comment, trackChanges: false);


            return Ok(result);
        }

        /// <summary>
        /// Get Waiting post (Role: Manager, Member)
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("waiting")]
        public async Task<IActionResult> GetWaitingPost([FromQuery] PagingParameters paging)
        {
            var accountId = _userAccessor.getAccountId();
            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);
            Pagination<WaitingPost> result_no_image;
            if (account.Role == 1)
            {
                result_no_image = await _repositoryManager.Post.GetWaitingPost(trackChanges: false, paging);
            }
            else
            {
                result_no_image = await _repositoryManager.Post.GetWaitingPost(trackChanges: false, paging, accountId);
            }
            if (result_no_image == null) throw new ErrorDetails(HttpStatusCode.NotFound, "No waiting post was found");

            var result = await _repositoryManager.Image.GetImageForWaitingPostDetail(result_no_image, trackChanges: false);

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
            var account_id = _userAccessor.getAccountId();

            var result_no_comment_no_image = await _repositoryManager.Post.GetPostDetail(post_id, trackChanges: false, account_id);

            if (result_no_comment_no_image == null)
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No post matches with the id: " + post_id);

            var result_no_image = await _repositoryManager.Comment.GetPostComment(result_no_comment_no_image, trackChanges: false, account_id);

            var result = await _repositoryManager.Image.GetImageForPostDetail(result_no_image, trackChanges: false);

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
        /// Feedback Post (Role: Member)
        /// </summary>
        /// <param name="post_id">Post id want to feedback</param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{post_id}/feedback")]
        public async Task<IActionResult> FeedbackPost(int post_id, [FromBody]string content)
        {
            var sender_id = _userAccessor.getAccountId();

            var feedback = new Feedback
            {
                PostId = post_id,
                Content = content,
                SenderId = sender_id,
                SendDate = DateTime.UtcNow
            };

            _repositoryManager.Feedback.Create(feedback);
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
            bool isLiked = true;

            var post = await _repositoryManager.Post.GetPostReactById(post_id, trackChanges: false);

            var accountId = _userAccessor.getAccountId();

            if (post == null)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "No post matches with post id");

            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);

            if (account == null)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "No account matches with account id");

            //account is reacted
            var isReacted = _repositoryManager.Post.IsReactedPost(post, accountId);

            if (isReacted)
            {
                //un react
                _repositoryManager.ReactPost.DeleteReact(
                    new Entities.Models.ReactPost { AccountId = accountId, PostId = post_id });
                isLiked = false;
            }
            else
            {
                //react
                _repositoryManager.ReactPost.CreateReact(
                    new Entities.Models.ReactPost { AccountId = accountId, PostId = post_id });
                //Create Notification
                var user = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), false);
                var owner = await _repositoryManager.Post.GetOwnerByPostId(post_id);
                CreateNotificationModel noti = new CreateNotificationModel
                {
                    Content = user.Name + " has react your Post!",
                    AccountId = owner,
                    PostId = post_id,
                };
                _repositoryManager.Notification.CreateNotification(noti);
            }

            await _repositoryManager.SaveAsync();

            var numOfReact = await _repositoryManager.Post.GetNumOfReact(post_id, trackChanges: false);

            return Ok(new
            {
                Message = "Save changes success",
                NumOfReact = numOfReact,
                IsLiked = isLiked
            });
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
            //CREATE COMMENT
            CreateNotificationModel noti = new CreateNotificationModel
            {
                Content = "Your post is approved",
                AccountId = post.AccountId,
                PostId = post.Id,
            };
            //push notification to list follower
            var listFollower = await _repositoryManager.FollowAccount.GetAccountFollower((int)post.AccountId, false);
            foreach (var item in listFollower)
            {
                CreateNotificationModel notiToFollower = new CreateNotificationModel
                {
                    Content = await _repositoryManager.Account.GetAccountById((int)post.AccountId, false) + " have a new post!",
                    AccountId = item.Id,
                    PostId = post.Id,
                };
            }
            //END
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
        [HttpDelete]
        [Route("{post_id}")]
        public async Task<IActionResult> Delete(int post_id)
        {
            var current_account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            //Get all id to delete
            var post = await _repositoryManager.Post.GetDeletePost(post_id, trackChanges: false);

            if (post.AccountId != current_account.Id && current_account.Role != 1)
                throw new ErrorDetails(HttpStatusCode.BadRequest, "You don't have permission to delete");

            //Delete Image
            if (post.Images != null || post.Images.Count > 0)
            {
                foreach (var image in post.Images)
                {
                    _repositoryManager.Image.Delete(image);
                }
                await _repositoryManager.SaveAsync();
            }

            var commentList = post.Comments;

            //delete comment
            if (commentList != null || commentList.Count > 0)
            {
                foreach (var comment in commentList)
                {
                    //delete react comment
                    foreach (var reactComment in comment.ReactComments)
                    {
                        _repositoryManager.ReactComment.DeleteReact(reactComment);
                    }
                    await _repositoryManager.SaveAsync();
                    _repositoryManager.Comment.DeleteComment(comment);
                }
                await _repositoryManager.SaveAsync();
            }

            //Delete react post
            if (post.ReactPosts != null || post.ReactPosts.Count > 0)
            {
                foreach (var reactPost in post.ReactPosts)
                {
                    _repositoryManager.ReactPost.DeleteReact(reactPost);
                }
                await _repositoryManager.SaveAsync();
            }

            //delete post
            _repositoryManager.Post.Delete(post);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
    }
}
