using Contracts;
using Entities.DataTransferObject;
using Entities.ErrorModel;
using Entities.Models;
using Entities.RequestFeatures;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/contest")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUserAccessor _userAccessor;

        public ContestController(IRepositoryManager repositoryManager, IUserAccessor userAccessor)
        {
            _repositoryManager = repositoryManager;
            _userAccessor = userAccessor;
        }

        #region Get contest by status
        /// <summary>
        /// Get contest by status
        /// </summary>
        /// <param name="status">0: all, 1: closed, 2: order contest status</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("status/{status}")]
        public async Task<IActionResult> GetAllContest(int status, [FromQuery] PagingParameters paging)
        {
            var account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var contests = await _repositoryManager.Contest.GetContestByStatus(status, paging, trackChanges: false);
            if (contests == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest matches with input status");

            return Ok(contests);
        }
        #endregion

        #region Get contest by status mobile
        /// <summary>
        /// Get contest by status
        /// </summary>
        /// <param name="status">0: all, 1: closed, 2: order contest status</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("status/{status}/mobile")]
        public async Task<IActionResult> GetAllContestMb(int status, [FromQuery] PagingParameters paging)
        {
            var account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var contests = await _repositoryManager.Contest.GetContestByStatus(status, paging, trackChanges: false);
            if (contests == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest matches with input status");

            contests = await _repositoryManager.PrizeContest.GetPrizeForContest(contests);

            return Ok(contests);
        }
        #endregion

        #region Get highlight contest
        /// <summary>
        /// Get highlight contest for the home page (Role: Manager, Member)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("highlight")]
        public async Task<IActionResult> GetHighlightContest()
        {
            var result = await _repositoryManager.Contest.GetHightlightContest(trackChanges: false);

            if (result == null || result.Count() == 0)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest is available");
            }

            return Ok(result);
        }
        #endregion

        #region Get favorite cotnest
        /// <summary>
        /// Get favorite contest
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("favorite")]
        public async Task<IActionResult> GetFavoriteContest([FromQuery]PagingParameters paging)
        {
            var account_id = _userAccessor.getAccountId();

            //Get favorite type
            var types = await _repositoryManager.FavoriteType.GetFavoriteType(account_id, trackChanges: false);
            //Get favorite brand
            var brands = await _repositoryManager.FavoriteBrand.GetFavoriteBrand(account_id, trackChanges: false);
            //Get contest by type and brand
            var favorite_contests = await _repositoryManager.Contest.GetContestByBrandAndType(account_id, types, brands, paging, trackChanges: false);

            return Ok(favorite_contests);
        }
        #endregion

        #region Get favorite cotnest mobile
        /// <summary>
        /// Get favorite contest
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("favorite/mobile")]
        public async Task<IActionResult> GetFavoriteContestMb([FromQuery] PagingParameters paging)
        {
            var account_id = _userAccessor.getAccountId();

            //Get favorite type
            var types = await _repositoryManager.FavoriteType.GetFavoriteType(account_id, trackChanges: false);
            //Get favorite brand
            var brands = await _repositoryManager.FavoriteBrand.GetFavoriteBrand(account_id, trackChanges: false);
            //Get contest by type and brand
            var contest_no_prize = await _repositoryManager.Contest.GetContestByBrandAndType(account_id, types, brands, paging, trackChanges: false);
            var favorite_contests = await _repositoryManager.PrizeContest.GetPrizeForContest(contest_no_prize);

            return Ok(favorite_contests);
        }
        #endregion

        #region Check is account joined to contest
        /// <summary>
        /// Check is user attended to the contest (Role: Manager, Member)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{contest_id}/attended")]
        public async Task<IActionResult> CheckAccountInTheContest(int contest_id)
        {
            var account_id = _userAccessor.getAccountId();

            var isInContest = await _repositoryManager.JoinContest.IsJoinedToContest(contest_id, account_id, trackChanges: false);

            return Ok(new { IsJoinedToContest = isInContest });
        }
        #endregion

        #region Get contest by group id
        /// <summary>
        /// Get contest by group id (Role: Manager, Member)
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id}")]
        public async Task<IActionResult> GetContestByGroup(int group_id, [FromQuery] PagingParameters paging)
        {
            var account_id = _userAccessor.getAccountId();

            var result = await _repositoryManager.Contest.GetContestInGroup(group_id, account_id, trackChanges: false, paging);

            if (result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest is available");
            }

            return Ok(result);
        }
        #endregion

        #region Get contest by group id
        /// <summary>
        /// Get contest by group id (Role: Manager, Member)
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id}/mobile")]
        public async Task<IActionResult> GetContestByGroupMb(int group_id, [FromQuery] PagingParameters paging)
        {
            var account_id = _userAccessor.getAccountId();

            var contest_have_not_prize = await _repositoryManager.Contest.GetContestInGroup(group_id, account_id, trackChanges: false, paging);

            if (contest_have_not_prize == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest is available");
            }


            var result = await _repositoryManager.PrizeContest.GetPrizeForContest(contest_have_not_prize);

            return Ok(result);
        }
        #endregion

        #region Get contest detail
        /// <summary>
        /// Get detail information of contest (Role: Manager, Member)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{contest_id}/details")]
        public async Task<IActionResult> GetContestDetail(int contest_id)
        {
            var contest_detail = await _repositoryManager.Contest.GetContestDetailInformation(contest_id, trackChanges: false);

            if (contest_detail == null) return NotFound("No contest matches with the id");

            return Ok(contest_detail);
        }
        #endregion

        #region Get post of contest
        /// <summary>
        /// Get post of contest (Role: Manager, Member)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{contest_id}/posts")]
        public async Task<IActionResult> GetPostsOfContest(int contest_id, [FromQuery] PagingParameters paging)
        {
            var account_id = _userAccessor.getAccountId();

            var posts_no_rate_no_image = await _repositoryManager.PostOfContest.GetPostOfContest(contest_id, paging, account_id, trackChanges: false);

            if (posts_no_rate_no_image == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No post in this contest");

            var post_no_rate = await _repositoryManager.Image.GetImageForPostOfContest(posts_no_rate_no_image, trackChanges: false);

            var posts = await _repositoryManager.Rate.GetRateForPostOfContest(post_no_rate, account_id, trackChanges: false);

            return Ok(posts);
        }
        #endregion

        #region Get prize for contest
        /// <summary>
        /// Get prize for contest detail page (Role: Manager, Member)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{contest_id}/prizes")]
        public async Task<IActionResult> GetPrizeOfContest(int contest_id)
        {
            var rewards = await _repositoryManager.PrizeContest.GetPrizeForContestDetail(contest_id, trackChanges: false);

            if (rewards == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "This contest has not prize");

            return Ok(rewards);
        }
        #endregion

        #region Get reward for contest
        /// <summary>
        /// Get Reward of contest (Role: Manager, Member)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{contest_id}/rewards")]
        public async Task<IActionResult> GetRewardOfContest(int contest_id)
        {
            var rewards_post_no_image = await _repositoryManager.Reward.GetContestReward(contest_id, trackChanges: false);

            if (rewards_post_no_image == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "This contest has no reward");

            var rewards = await _repositoryManager.Image.GetImageForRewards(rewards_post_no_image, trackChanges: false);

            var result = new List<RewardReturn>();
            foreach (var reward in rewards)
            {
                reward.Prizes = await _repositoryManager.Image.GetImageForPrize(reward.Prizes, trackChanges: false);
                result.Add(reward);
            }

            return Ok(result);
        }
        #endregion

        #region Get list subcribers
        /// <summary>
        /// Get list subscribers of contest (Role: Manager)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{contest_id}/subscribers")]
        public async Task<IActionResult> GetListSubscribers(int contest_id)
        {
            var subscribers = await _repositoryManager.JoinContest.GetListSubscribers(contest_id, trackChanges: false);

            if (subscribers == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No subscribers in this contest");

            return Ok(subscribers);
        }
        #endregion

        #region Get list Brand
        /// <summary>
        /// Get brand to create contest (Role: Manager)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("create/brand")]
        public async Task<IActionResult> GetBrandCreateContest()
        {
            var brands = await _repositoryManager.Brand.GetBrandCreateContest(trackChanges: false);

            return Ok(brands);
        }
        #endregion

        #region Get list type
        /// <summary>
        /// Get type to create contest (Role: Manager)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("create/type")]
        public async Task<IActionResult> GetTypeCreateContest()
        {
            var types = await _repositoryManager.Type.GetTypeCreateContest(trackChanges: false);

            return Ok(types);
        }
        #endregion

        #region Get top runner
        /// <summary>
        /// Get top 3 post have highest sum of star (Role: Member, Manager)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{contest_id}/top_3")]
        public async Task<IActionResult> GetTopSubmission(int contest_id)
        {
            var submissionsId = await _repositoryManager.PostOfContest.GetIdOfPost(contest_id, trackChanges: false);

            if (submissionsId.Count == 0) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No post in this contest");

            var idsPostInTop = await _repositoryManager.Rate.GetIdOfPostInTop(submissionsId, trackChanges: false);

            var posts = await _repositoryManager.PostOfContest.GetPostOfContestById(idsPostInTop, trackchanges: false);
            posts = await _repositoryManager.Rate.GetRateForPostOfContest(posts, trackChanges: false);
            posts = await _repositoryManager.Image.GetImageForPostOfContest(posts, trackChanges: false);

            return Ok(posts);
        }
        #endregion

        #region Remove subscriber
        /// <summary>
        /// Remove subscribers of contest (Role: Manager)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <param name="account_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{contest_id}/subscribers/{account_id}")]
        public async Task<IActionResult> RemoveSubscribers(int contest_id, int account_id)
        {
            var account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);
            if (account.Role != 1) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to remove");

            var joinContest = await _repositoryManager.JoinContest.GetSubsCriberToDelete(contest_id, account_id, trackChanges: false);

            _repositoryManager.JoinContest.BandSubscribers(joinContest);

            var posts = await _repositoryManager.PostOfContest.GetPostToDelete(contest_id, account_id, trackChanges: false);
            
            foreach(var post in posts)
            {
                await DeletePostOfContest(post.Id);
            }

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Delete post of contest
        [HttpDelete]
        [Route("postofcontest/{post_of_contest_id}")]
        public async Task<IActionResult> DeletePostOfContest(int post_of_contest_id)
        {
            var account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            var post = await _repositoryManager.PostOfContest.GetPostOfContestById(post_of_contest_id, trackchanges: false);
            if (account.Role != 1 && post.AccountId != post.AccountId)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to delete");

            //delete rate
            await _repositoryManager.Rate.DeleteRateOfPost(post, trackChanges: true);

            //delete feedback
            await _repositoryManager.Feedback.DeleteByPostOfContestId(post.Id, trackChanges: true);

            //delete notification
            await _repositoryManager.Notification.DeleteByPostOfContestId(post.Id, trackChanges: true);

            //delete image
            await _repositoryManager.Image.DeleteByPostOfContestId(post.Id, trackChanges: true);

            _repositoryManager.PostOfContest.Delete(post);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Delete contest
        /// <summary>
        /// Delete contest (Role: Manager)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{contest_id}")]
        public async Task<IActionResult> DeleteContest(int contest_id)
        {
            var account = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);
            if (account.Role != 1) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to remove");

            //Remove prize contest
            await _repositoryManager.PrizeContest.Delete(contest_id, trackChanges: false);
            //Remove reward
            await _repositoryManager.Reward.Delete(contest_id, trackChanges: false);
            //Remove evaluate
            await _repositoryManager.EvaluateContest.Delete(contest_id, trackChanges: false);
            //Remove post of contest and rate
            var listPostId = await _repositoryManager.PostOfContest.GetPostOfContest(contest_id, trackChanges: false);
            await _repositoryManager.Rate.Delete(listPostId, trackChanges: false);
            await _repositoryManager.PostOfContest.Delete(contest_id, trackChanges: false);
            //Remove subcribers
            await _repositoryManager.JoinContest.Delete(contest_id, trackChanges: false);
            //Remove notification
            await _repositoryManager.Notification.Delete(contest_id, trackChanges: false);
            await _repositoryManager.SaveAsync();
            //Remove contest
            await _repositoryManager.Contest.Delete(contest_id, trackChanges: false);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region End contest
        /// <summary>
        /// End contest (Role: Manager)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{contest_id}/end")]
        public async Task<IActionResult> EndContest(int contest_id)
        {
            //get list prize descending by value sort des prize
            var prizesList = await _repositoryManager.PrizeContest.GetPrizeForEndContest(contest_id, trackChanges: false);

            //Get list post of contest count star sort des prize
            var postsOfContestList = await _repositoryManager.PostOfContest.GetPostOfContestForEndContest(contest_id, trackChanges: false);

            //For prize get highest star contest
            foreach (var prize in prizesList)
            {
                var post = postsOfContestList.First();
                if (post != null)
                {
                    _repositoryManager.Reward.Create(new Reward
                    {
                        AccountId = post.AccountId,
                        ContestId = contest_id,
                        PostOfContestId = post.Id,
                        PrizeId = prize.Id
                    });
                }
                else
                {
                    break;
                }
                //Bỏ highest ra for prize tiếp
                postsOfContestList.Remove(post);
            }

            await _repositoryManager.Contest.EndContest(contest_id, trackChanges: false);

            await _repositoryManager.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion

        #region Create post of contest
        /// <summary>
        /// Create post in contest, call after check is user in the contest (Role: Manager, Member)
        /// </summary>
        /// <param name="param"></param>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{contest_id}/post")]
        public async Task<IActionResult> CreatePostOfContest(NewPostOfContestParameters param, int contest_id)
        {
            var accountId = _userAccessor.getAccountId();

            var isOpenContest = await _repositoryManager.Contest.IsOpenContest(contest_id, trackChanges: false);
            if (!isOpenContest) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "This contest is not open for post");

            var isReachLimit = await _repositoryManager.PostOfContest.IsReachPostLimit(accountId, contest_id, trackChanges: false);
            if (isReachLimit) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "User have reach the limit of submission");

            var postOfContest = new PostOfContest
            {
                AccountId = accountId,
                Content = param.Content,
                ContestId = contest_id,
                Images = param.ImagesUrl.Select(x => new Image { Url = x }).ToList(),
                DateCreate = DateTime.UtcNow
            };

            _repositoryManager.PostOfContest.Create(postOfContest);
            await _repositoryManager.SaveAsync();

            return Ok("Save change success");
        }
        #endregion

        #region Create contest
        /// <summary>
        /// Create contest (Role: Manager)
        /// </summary>
        /// <param name="param"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("group/{group_id}")]
        public async Task<IActionResult> CreateContest(CreateContestParameters param, int group_id)
        {
            DateTime startDate, endDate, startRegis, endRegis;

            //var brand = await _repositoryManager.Brand
            //    .GetBrandByName(param.BrandName == null ? "Unknow Brand" : param.BrandName, trackChanges: false);
            //if (brand == null)
            //{
            //    _repositoryManager.Brand.CreateBrand(new Brand { Name = param.BrandName });
            //    await _repositoryManager.SaveAsync();
            //    brand = await _repositoryManager.Brand.GetBrandByName(param.BrandName, trackChanges: false);
            //}

            var type = await _repositoryManager.Type
                .GetTypeByName(param.TypeName == null ? "Unknow Type" : param.TypeName, trackChanges: false);
            if (type == null)
            {
                _repositoryManager.Type.CreateType(new Entities.Models.Type { Name = param.TypeName });
                await _repositoryManager.SaveAsync();
                //type = await _repositoryManager.Type.GetTypeByName(param.BrandName, trackChanges: false);
            }

            var contest = new Contest
            {
                Title = param.Title,
                Description = param.Description,
                CoverImage = param.CoverImage,
                Slogan = param.Slogan,
                //MinRegistration = param.MinRegistration,
                //MaxRegistration = param.MaxRegistration,
                StartRegistration = param.StartRegistration,
                EndRegistration = param.EndRegistration,
                StartDate = param.StartDate,
                EndDate = param.EndDate,
                GroupId = group_id,
                //BrandId = brand.Id,
                TypeId = type.Id,
                Status = 0,
                Rule = param.Rule
            };

            _repositoryManager.Contest.Create(contest); //created contest
            await _repositoryManager.SaveAsync(); //inserted to DB

            var createdContest = await _repositoryManager.Contest.GetCreatedContest(group_id, param.Title, param.StartRegistration, trackChanges: false);

            //schedule for contest
            startRegis = DateTime.SpecifyKind(param.StartRegistration.Value, DateTimeKind.Local).AddHours(-7);
            BackgroundJob.Schedule(() => StartRegisContest(createdContest.Id), startRegis);
            endRegis = DateTime.SpecifyKind(param.EndRegistration.Value, DateTimeKind.Local).AddHours(-7);
            BackgroundJob.Schedule(() => ClosedRegisContest(createdContest.Id), endRegis);
            startDate = DateTime.SpecifyKind(param.StartDate.Value, DateTimeKind.Local).AddHours(-7);
            BackgroundJob.Schedule(() => OpenContest(createdContest.Id), startDate);
            endDate = DateTime.SpecifyKind(param.EndDate.Value, DateTimeKind.Local).AddHours(-7);
            BackgroundJob.Schedule(() => ClosedContest(createdContest.Id), endDate);

            //CREATE NOTIFICATION
            var users = await _repositoryManager.FollowGroup.GetUserFollowGroup(group_id);
            foreach (var user in users)
            {
                CreateNotificationModel noti = new CreateNotificationModel
                {
                    Content = "Contest " + param.Title + " is Created!",
                    AccountId = user.AccountId,
                    ContestId = createdContest.Id,
                };
                _repositoryManager.Notification.CreateNotification(noti);
            }
            //END
            await _repositoryManager.SaveAsync();
            return Ok(new { contestId = createdContest.Id });
        }
        #endregion

        #region Evaluate contest
        /// <summary>
        /// Evaluate contest (Role: Member, manager => Only user joined to contest)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{contest_id}/evaluate")]
        public async Task<IActionResult> EvaluateContest(int contest_id, NewEvaluateContestParameters param)
        {
            var contest = await _repositoryManager.Contest.GetEvaluateContest(contest_id, trackChanges: false);

            var current_accountId = _userAccessor.getAccountId();

            var isJoinContest = contest.AccountJoined.Where(x => x.AccountId == current_accountId).ToList().Count() > 0;
            if (!isJoinContest)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Only people joined to contest can evaluate");

            var evaluate = new Evaluate
            {
                AccountId = current_accountId,
                Comment = param.Comment,
                ContestId = contest_id,
                NoOfStart = param.NumOfStar
            };
            _repositoryManager.EvaluateContest.Create(evaluate);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Feedback post of contest
        /// <summary>
        /// Feedback post of contest (Role: Member)
        /// </summary>
        /// <param name="post_of_contest_id">Post of contest you want to feedback</param>
        /// <param name="newFeedback"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("post_of_contest/{post_of_contest_id}/feedback")]
        public async Task<IActionResult> FeedbackPost(int post_of_contest_id, NewFeedback newFeedback)
        {
            var sender_id = _userAccessor.getAccountId();

            var feedback = new Feedback
            {
                PostOfContestId = post_of_contest_id,
                Content = newFeedback.Content,
                SenderId = sender_id,
                SendDate = DateTime.UtcNow
            };

            _repositoryManager.Feedback.Create(feedback);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Start regis contest
        [HttpPut("startregis")]
        public void StartRegisContest(int contest_id)
        {
            _repositoryManager.Contest.StartRegistration(contest_id, trackChanges: false).Wait();
            _repositoryManager.SaveAsync().Wait();
        }
        #endregion

        #region End regist contest
        [HttpPut("endregis")]
        public void ClosedRegisContest(int contest_id)
        {
            _repositoryManager.Contest.EndRegistration(contest_id, trackChanges: false).Wait();
            _repositoryManager.SaveAsync().Wait();
        }
        #endregion

        #region Open contest
        [HttpPut("open")]
        public void OpenContest(int contest_id)
        {
            _repositoryManager.Contest.StartContest(contest_id, trackChanges: false).Wait();
            _repositoryManager.SaveAsync().Wait();
        }
        #endregion

        #region Close contest
        [HttpPut("closed")]
        public void ClosedContest(int contest_id)
        {
            _repositoryManager.Contest.EndContest(contest_id, trackChanges: false).Wait();
            _repositoryManager.SaveAsync().Wait();
        }
        #endregion

        #region Join to contest
        /// <summary>
        /// Join to contest (Role: Manager, Member)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{contest_id}/join")]
        public async Task<IActionResult> JoinToContest(int contest_id)
        {
            var account_id = _userAccessor.getAccountId();

            bool is_start_regis = await _repositoryManager.Contest.IsStartRegis(contest_id, trackChanges: false);

            if (!is_start_regis)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Contest has closed attemp");
            }

            bool IsBan = await _repositoryManager.JoinContest.IsBan(contest_id, account_id, trackChanges: false);

            if (IsBan) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Your account is ban from this contest");

            _repositoryManager.JoinContest.Create(
                new JoinedToContest
                {
                    AccountId = account_id,
                    ContestId = contest_id,
                    IsBan = false
                });
            await _repositoryManager.SaveAsync();

            return Ok("Save change success");
        }
        #endregion

        #region Rate post of contest
        /// <summary>
        /// Rate post of contest (Role: Manager, Member)
        /// </summary>
        /// <param name="post_of_contest_id"></param>
        /// <param name="parameters"></param>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{contest_id}/rate/{post_of_contest_id}")]
        public async Task<IActionResult> RateTheContest(int post_of_contest_id, int contest_id, RateSubmissionParameters parameters)
        {
            var account_id = _userAccessor.getAccountId();

            var isRated = await _repositoryManager.Rate.IsRated(post_of_contest_id, account_id, trackChanges: false);

            var isOpenContest = await _repositoryManager.Contest.IsOpenContest(contest_id, trackChanges: false);

            if (!isOpenContest) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Closed contest");

            if (isRated) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Already rated this post");

            var rate = new Rate
            {
                AccountId = account_id,
                Note = parameters.Note,
                NumOfStar = parameters.NumOfStar,
                PostOfContestId = post_of_contest_id
            };

            _repositoryManager.Rate.Create(rate);

            //Create notification
            var user = await _repositoryManager.Account.GetAccountById(_userAccessor.getAccountId(), false);
            CreateNotificationModel noti = new CreateNotificationModel
            {
                Content = user.Name + " has react your post!",
                AccountId = await _repositoryManager.PostOfContest.GetOwnerByPostOfContestId(post_of_contest_id),
                PostOfContestId = post_of_contest_id,
            };
            _repositoryManager.Notification.CreateNotification(noti);

            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Add prize to contest
        /// <summary>
        /// Add prizes to contest
        /// </summary>
        /// <param name="contest_id">Contest id return after create contest success</param>
        /// <param name="prizes_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{contest_id}/prizes")]
        public async Task<IActionResult> AddPrizeToContest(int contest_id, List<int> prizes_id)
        {
            foreach (var prizeId in prizes_id)
            {
                _repositoryManager.PrizeContest.Create(new PrizeContest { ContestId = contest_id, PrizeId = prizeId });
            }
            await _repositoryManager.SaveAsync();

            return Ok("Saves change success");
        }
        #endregion
    }
}
