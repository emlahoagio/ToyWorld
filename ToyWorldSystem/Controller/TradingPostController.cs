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
    [Route("api/trading_posts")]
    [ApiController]
    public class TradingPostController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUserAccessor _userAccessor;

        public TradingPostController(IRepositoryManager repositoryManager, IUserAccessor userAccessor)
        {
            _repositoryManager = repositoryManager;
            _userAccessor = userAccessor;
        }

        #region Get trading by group id
        /// <summary>
        /// Get list trading post (Role: Manager, Member)
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id}")]
        public async Task<IActionResult> GetListTradingPost([FromQuery] PagingParameters paging, int group_id)
        {
            var account_id = _userAccessor.getAccountId();

            var result = await _repositoryManager.TradingPost
                    .GetTradingPostInGroupMember(group_id, paging, trackChanges: false, account_id);

            if (result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more posts in this group");
            }

            return Ok(result);
        }
        #endregion

        #region Get trading by group id mobile
        /// <summary>
        /// Get list trading post (Role: Manager, Member)
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id}/mobile")]
        public async Task<IActionResult> GetListTradingPostMb([FromQuery] PagingParameters paging, int group_id)
        {
            var account_id = _userAccessor.getAccountId();

            var result = await _repositoryManager.TradingPost
                    .GetTradingPostInGroupMember(group_id, paging, trackChanges: false, account_id);

            if (result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more posts in this group");
            }

            result = await _repositoryManager.Image.GetImageForListTradingPost(result, trackChanges: false);

            result = await _repositoryManager.Comment.GetNumOfCommentForTradingPostList(result, trackChanges: false);

            return Ok(result);
        }
        #endregion

        #region Get images of trading post
        [HttpGet]
        [Route("{trading_id}/images")]
        public async Task<IActionResult> GetImagesByPostId(int trading_id)
        {
            var images = await _repositoryManager.Image.GetImageByTradingPostId(trading_id, trackChanges: false);

            if (images == null) throw new ErrorDetails(HttpStatusCode.NotFound, "No image in post");

            return Ok(images);
        }
        #endregion

        #region Get num of comment for trading post
        /// <summary>
        /// Get num of comment for post
        /// </summary>
        /// <param name="post_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{post_id}/num_of_comment")]
        public async Task<IActionResult> GetNumOfComment(int post_id)
        {
            var numOfComment = await _repositoryManager.Comment.GetNumOfCommentByTradingId(post_id, trackChanges: false);

            return Ok(numOfComment);
        }
        #endregion

        #region Get favorite trading post
        /// <summary>
        /// Get favorite trading post for home page
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("favorite")]
        public async Task<IActionResult> GetFavoriteTradingPost([FromQuery] PagingParameters paging)
        {
            var account_id = _userAccessor.getAccountId();

            //Get favorite type
            var types = await _repositoryManager.FavoriteType.GetFavoriteType(account_id, trackChanges: false);
            //Get favorite brand
            var brands = await _repositoryManager.FavoriteBrand.GetFavoriteBrand(account_id, trackChanges: false);
            //Get contest by type and brand
            var trading = await _repositoryManager.TradingPost.GetTradingByBrandAndType(account_id, types, brands, paging, trackChanges: false);

            if (trading == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more posts in this group");
            }

            return Ok(trading);
        }
        #endregion

        #region Get favorite trading post
        /// <summary>
        /// Get favorite trading post for home page
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("favorite/mobile")]
        public async Task<IActionResult> GetFavoriteTradingPostMb([FromQuery] PagingParameters paging)
        {
            var account_id = _userAccessor.getAccountId();

            //Get favorite type
            var types = await _repositoryManager.FavoriteType.GetFavoriteType(account_id, trackChanges: false);
            //Get favorite brand
            var brands = await _repositoryManager.FavoriteBrand.GetFavoriteBrand(account_id, trackChanges: false);
            //Get contest by type and brand
            var trading = await _repositoryManager.TradingPost.GetTradingByBrandAndType(account_id, types, brands, paging, trackChanges: false);

            if (trading == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more posts in this group");
            }

            trading = await _repositoryManager.Image.GetImageForListTradingPost(trading, trackChanges: false);

            trading = await _repositoryManager.Comment.GetNumOfCommentForTradingPostList(trading, trackChanges: false);

            return Ok(trading);
        }
        #endregion

        #region Get trading by isdisable
        /// <summary>
        /// Get trading post by disable status (Role: Manager)
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="status">0: All, 1: disabled, 2: enable</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Status/{status}")]
        public async Task<IActionResult> GetTradingByStatus([FromQuery] PagingParameters paging, int status)
        {
            if (status < 0 && status > 2) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid status");

            var accountId = _userAccessor.getAccountId();
            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);

            if (account.Role != 1) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to get");

            var result = await _repositoryManager.TradingPost
                .GetTradingPostForManager(status, paging, trackChanges: false, accountId);

            if (result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more posts in this group");
            }

            result = await _repositoryManager.ReactTradingPost.GetIsReactedReactTrading(result, accountId, trackChanges: false);

            return Ok(result);
        }
        #endregion

        #region Get trading by isdisable mobile
        /// <summary>
        /// Get trading post by disable status (Role: Manager)
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="status">0: All, 1: disabled, 2: enable</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Status/{status}/mobile")]
        public async Task<IActionResult> GetTradingByStatusMb([FromQuery] PagingParameters paging, int status)
        {
            if (status < 0 && status > 2) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid status");

            var accountId = _userAccessor.getAccountId();
            var account = await _repositoryManager.Account.GetAccountById(accountId, trackChanges: false);

            if (account.Role != 1) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to get");

            var result = await _repositoryManager.TradingPost
                .GetTradingPostForManager(status, paging, trackChanges: false, accountId);

            if (result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more posts in this group");
            }

            result = await _repositoryManager.ReactTradingPost.GetIsReactedReactTrading(result, accountId, trackChanges: false);

            result = await _repositoryManager.Image.GetImageForListTradingPost(result, trackChanges: false);

            result = await _repositoryManager.Comment.GetNumOfCommentForTradingPostList(result, trackChanges: false);

            return Ok(result);
        }
        #endregion

        #region Get update information trading
        /// <summary>
        /// Get detail of trading post to update (Role: Manager, Member)
        /// </summary>
        /// <param name="tradingpost_id">Id of trading post need to update</param>
        /// <returns></returns>
        [HttpGet]
        [Route("update/{tradingpost_id}")]
        public async Task<IActionResult> GetTradingPostToUpdateInformation(int tradingpost_id)
        {
            var current_login_account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var update_tradingpost = await _repositoryManager.TradingPost.GetTradingPostById(tradingpost_id, trackChanges: false);

            if (update_tradingpost == null) return NotFound();

            if (update_tradingpost.AccountId != current_login_account.Id)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid request");

            var result = new UpdateTradingPost
            {
                Address = update_tradingpost.Address,
                Value = update_tradingpost.Value,
                ToyName = update_tradingpost.ToyName,
                Title = update_tradingpost.Title,
                Content = update_tradingpost.Content,
                Exchange = update_tradingpost.Trading,
                Phone = update_tradingpost.Phone
            };

            return Ok(result);
        }
        #endregion

        #region Get account information to create trading post
        /// <summary>
        /// Get information to create new trading post page (Role: Manager, Member)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("init_to_create")]
        public async Task<IActionResult> LoadInformationForCreate()
        {
            var toyNames = await _repositoryManager.Toy.GetNameOfToy(trackChanges: false);

            var currentAccount = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var result = new InitNewTradingPost
            {
                PhoneNumber = currentAccount.Phone,
                ToyNames = toyNames
            };

            return Ok(result);
        }
        #endregion

        #region Get trading post detail
        /// <summary>
        /// Get trading post detail (Role: Manager, Member)
        /// </summary>
        /// <param name="trading_post_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{trading_post_id}/detail")]
        public async Task<IActionResult> GetTradingPostDetail(int trading_post_id)
        {
            var current_account_id = _userAccessor.getAccountId();

            var trading_post_detail =
                await _repositoryManager.TradingPost.GetDetail(trading_post_id, current_account_id, trackChanges: false);

            if (trading_post_detail == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "Invalid trading post Id");

            return Ok(trading_post_detail);
        }
        #endregion

        #region Get trading post detail mobile
        /// <summary>
        /// Get trading post detail (Role: Manager, Member)
        /// </summary>
        /// <param name="trading_post_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{trading_post_id}/detail/mobile")]
        public async Task<IActionResult> GetTradingPostDetailMb(int trading_post_id)
        {
            var current_account_id = _userAccessor.getAccountId();

            var trading_post_detail =
                await _repositoryManager.TradingPost.GetDetail(trading_post_id, current_account_id, trackChanges: false);

            if (trading_post_detail == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "Invalid trading post Id");

            trading_post_detail = await 
                _repositoryManager.Image.GetImageForTradingDetail(trading_post_detail, trackChanges: false);

            trading_post_detail = await _repositoryManager.Comment.GetTradingComment(trading_post_detail, current_account_id, trackChanges: false);

            return Ok(trading_post_detail);
        }
        #endregion

        #region Get comment for trading detail
        /// <summary>
        /// Get comment for trading post detail page
        /// </summary>
        /// <param name="post_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{post_id}/comment_detail")]
        public async Task<IActionResult> GetDetailComment(int post_id)
        {
            var result = await _repositoryManager.Comment.GetCommentDetailOfTradingPost(_userAccessor.getAccountId(), post_id, trackChanges: false);

            return Ok(result);
        }
        #endregion

        #region Create trading post
        /// <summary>
        /// Create new trading post (Role: Manager, Member)
        /// </summary>
        /// <param name="tradingPost"></param>
        /// <param name="group_id">Id of group content trading post</param>
        /// <returns></returns>
        [HttpPost]
        [Route("group/{group_id}")]
        public async Task<IActionResult> CreateTradingPost([FromBody] NewTradingPostParameters tradingPost, int group_id)
        {
            var createTime = DateTime.UtcNow.AddHours(7);
            var account_id = _userAccessor.getAccountId();

            var toy = await _repositoryManager.Toy.GetToyByName(tradingPost.ToyName, trackChanges: false);

            var brand = await _repositoryManager.Brand
                .GetBrandByName(tradingPost.BrandName == null ? "Unknow Brand" : tradingPost.BrandName, trackChanges: false);
            if (brand == null)
            {
                _repositoryManager.Brand.CreateBrand(new Brand { Name = tradingPost.BrandName });
                await _repositoryManager.SaveAsync();
                brand = await _repositoryManager.Brand.GetBrandByName(tradingPost.BrandName, trackChanges: false);
            }

            var type = await _repositoryManager.Type
                .GetTypeByName(tradingPost.TypeName == null ? "Unknow Type" : tradingPost.TypeName, trackChanges: false);
            if (type == null)
            {
                _repositoryManager.Type.CreateType(new Entities.Models.Type { Name = tradingPost.TypeName });
                await _repositoryManager.SaveAsync();
                type = await _repositoryManager.Type.GetTypeByName(tradingPost.BrandName, trackChanges: false);
            }

            if (toy != null)
            {
                _repositoryManager.TradingPost.CreateTradingPost(tradingPost, group_id, account_id, toy.Id, brand.Id, type.Id, createTime);
            }
            else
            {
                _repositoryManager.TradingPost.CreateTradingPost(tradingPost, group_id, account_id, 3, brand.Id, type.Id, createTime);
            }
            await _repositoryManager.SaveAsync();

            //Create Notifications
            var users = await _repositoryManager.FollowGroup.GetUserFollowGroup(group_id);
            var account = await _repositoryManager.Account.GetAccountById(account_id, false);
            var createdTradingPostId = await _repositoryManager.TradingPost.GetIdOfCreatedTrading(createTime, trackChanges: false);
            foreach (var user in users)
            {
                CreateNotificationModel noti = new CreateNotificationModel
                {
                    Content = account.Name + " post a trading post in group",
                    AccountId = user.AccountId,
                    TradingPostId = createdTradingPostId,
                };
                _repositoryManager.Notification.CreateNotification(noti);
            }

            return Ok("Save changes success");
        }
        #endregion

        #region Create feedback trading post
        /// <summary>
        /// Feedback trading post (Role: Member)
        /// </summary>
        /// <param name="trading_post_id">Trading post you want to feedback</param>
        /// <param name="newFeedback"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{trading_post_id}/feedback")]
        public async Task<IActionResult> FeedbackPost(int trading_post_id, NewFeedback newFeedback)
        {
            var sender_id = _userAccessor.getAccountId();

            var feedback = new Feedback
            {
                TradingPostId = trading_post_id,
                Content = newFeedback.Content,
                SenderId = sender_id,
                SendDate = DateTime.UtcNow
            };

            _repositoryManager.Feedback.Create(feedback);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region React trading post
        /// <summary>
        /// React trading post (Role: Manager, Member)
        /// </summary>
        /// <param name="trading_post_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{trading_post_id}/react")]
        public async Task<IActionResult> ReactTradingPost(int trading_post_id)
        {
            var account_id = _userAccessor.getAccountId();

            var reactTrading = await _repositoryManager.ReactTradingPost.FindReact(trading_post_id, account_id, trackChanges: false);

            var isLiked = false;

            if (reactTrading == null)
            {
                _repositoryManager.ReactTradingPost.Create(new ReactTradingPost
                {
                    AccountId = account_id,
                    TradingPostId = trading_post_id
                });
                //Push notification to trading post owner
                isLiked = true;
            }
            else
                _repositoryManager.ReactTradingPost.Delete(reactTrading);

            await _repositoryManager.SaveAsync();

            var numOfReact = await _repositoryManager.TradingPost.GetNumOfReact(trading_post_id, trackChanges: false);

            return Ok(new
            {
                Message = "Save changes success",
                NumOfReact = numOfReact,
                IsLiked = isLiked
            });
        }
        #endregion

        #region Update trading post to exchanged
        /// <summary>
        /// Update trading post to is exchanged (Role: Manager, Member)
        /// </summary>
        /// <param name="tradingpost_id">id of post want to update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("exchanged/{tradingpost_id}")]
        public async Task<IActionResult> UpdateExchangedTradingPost(int tradingpost_id)
        {
            var current_login_account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var update_tradingpost = await _repositoryManager.TradingPost.GetTradingPostById(tradingpost_id, trackChanges: false);

            if (update_tradingpost.AccountId != current_login_account.Id)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid request");

            _repositoryManager.TradingPost.ExchangedTradingPost(update_tradingpost);

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Update information of trading
        /// <summary>
        /// Update all information of trading post
        /// </summary>
        /// <param name="update_infor"></param>
        /// <param name="tradingpost_id">Id of trading post need to update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{tradingpost_id}")]
        public async Task<IActionResult> UpdateAllInformationTradingPost([FromBody] UpdateTradingPostParameters update_infor, int tradingpost_id)
        {
            var current_login_account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var update_tradingpost = await _repositoryManager.TradingPost.GetTradingPostById(tradingpost_id, trackChanges: false);

            if (update_tradingpost.AccountId != current_login_account.Id)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid request");

            if (update_infor.ToyName != update_tradingpost.ToyName)
            {
                var newToyOfTrading = await _repositoryManager.Toy.GetToyByName(update_infor.ToyName, trackChanges: false);
                if (newToyOfTrading == null)
                {
                    update_tradingpost.ToyId = 3;
                }
                else
                {
                    update_tradingpost.ToyId = newToyOfTrading.Id;
                }
            }

            _repositoryManager.TradingPost.UpdateTradingPost(update_infor, update_tradingpost);

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Disable trading post
        /// <summary>
        /// Disable trading post
        /// </summary>
        /// <param name="tradingpost_id">id of trading post need to disable</param>
        /// <param name="disable_or_enable">0: disable, 1: enable</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{tradingpost_id}/{disable_or_enable}")]
        public async Task<IActionResult> DisableTradingPost(int tradingpost_id, int disable_or_enable)
        {
            var delete_post = await _repositoryManager.TradingPost.GetTradingPostById(tradingpost_id, trackChanges: false);

            if (disable_or_enable != 0 && disable_or_enable != 1)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid status change");

            var account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            if (delete_post.AccountId != account.Id && account.Role != 1)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to update");

            _repositoryManager.TradingPost.DisableOrEnable(delete_post, disable_or_enable);

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion
    }
}
