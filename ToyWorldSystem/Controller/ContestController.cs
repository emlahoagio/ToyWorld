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

            contests = await _repositoryManager.PrizeContest.GetPrizeForContest(contests);

            return Ok(contests);
        }

        /// <summary>
        /// Get highlight contest for the home page (Role: Manager, Member)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("highlight")]
        public async Task<IActionResult> GetHighlightContest()
        {
            var result = await _repositoryManager.Contest.getHightlightContest(trackChanges: false);

            if (result == null || result.Count() == 0)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest is available");
            }

            return Ok(result);
        }

        /// <summary>
        /// Get favorite contest
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("favorite")]
        public async Task<IActionResult> GetFavoriteContest(PagingParameters paging)
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

            var contest_have_not_prize = await _repositoryManager.Contest.GetContestInGroup(group_id, account_id, trackChanges: false, paging);

            var result = await _repositoryManager.PrizeContest.GetPrizeForContest(contest_have_not_prize);

            if (result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No contest is available");
            }

            return Ok(result);
        }

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
            foreach(var reward in rewards)
            {
                reward.Prizes = await _repositoryManager.Image.GetImageForPrize(reward.Prizes, trackChanges: false);
                result.Add(reward);
            }

            return Ok(result);
        }

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

            _repositoryManager.JoinContest.Delete(joinContest);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }

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

            if (contest.StartRegistration.Value.Day == DateTime.UtcNow.Day)
            {
                contest.Status = 1;
            }
            _repositoryManager.Contest.Create(contest); //created contest
            await _repositoryManager.SaveAsync(); //inserted to DB

            var createdContest = await _repositoryManager.Contest.GetCreatedContest(group_id, param.Title, param.StartRegistration, trackChanges: false);

            //schedule for contest
            if (contest.StartRegistration.Value.Day != DateTime.UtcNow.Day)
            {
                startRegis = contest.StartRegistration.Value;
                BackgroundJob.Schedule(() => StartRegisContest(createdContest.Id), DateTime.SpecifyKind(startRegis, DateTimeKind.Local));
            }
            endRegis = contest.EndRegistration.Value;
            BackgroundJob.Schedule(() => ClosedRegisContest(createdContest.Id), DateTime.SpecifyKind(endRegis, DateTimeKind.Local));
            startDate = contest.StartDate.Value;
            BackgroundJob.Schedule(() => OpenContest(createdContest.Id), DateTime.SpecifyKind(startDate, DateTimeKind.Local));
            endDate = contest.EndDate.Value;
            BackgroundJob.Schedule(() => ClosedContest(createdContest.Id), DateTime.SpecifyKind(endDate, DateTimeKind.Local));

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

        /// <summary>
        /// Feedback post of contest (Role: Member)
        /// </summary>
        /// <param name="post_of_contest_id">Post of contest you want to feedback</param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("post_of_contest/{post_of_contest_id}/feedback")]
        public async Task<IActionResult> FeedbackPost(int post_of_contest_id, string content)
        {
            var sender_id = _userAccessor.getAccountId();

            var feedback = new Feedback
            {
                PostOfContestId = post_of_contest_id,
                Content = content,
                SenderId = sender_id,
                SendDate = DateTime.UtcNow
            };

            _repositoryManager.Feedback.Create(feedback);
            await _repositoryManager.SaveAsync();

            return Ok("Save changes success");
        }

        [HttpPut("startregis")]
        public void StartRegisContest(int contest_id)
        {
            _repositoryManager.Contest.StartRegistration(contest_id, trackChanges: false).Wait();
            _repositoryManager.SaveAsync().Wait();
        }

        [HttpPut("endregis")]
        public void ClosedRegisContest(int contest_id)
        {
            _repositoryManager.Contest.EndRegistration(contest_id, trackChanges: false).Wait();
            _repositoryManager.SaveAsync().Wait();
        }

        [HttpPut("open")]
        public void OpenContest(int contest_id)
        {
            _repositoryManager.Contest.StartContest(contest_id, trackChanges: false).Wait();
            _repositoryManager.SaveAsync().Wait();
        }

        [HttpPut("closed")]
        public void ClosedContest(int contest_id)
        {
            _repositoryManager.Contest.EndContest(contest_id, trackChanges: false).Wait();
            _repositoryManager.SaveAsync().Wait();
        }

        /// <summary>
        /// Join to contest after payment (Role: Manager, Member)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{contest_id}/join")]
        public async Task<IActionResult> JoinToContest(int contest_id)
        {
            var account_id = _userAccessor.getAccountId();

            bool can_attemp = await _repositoryManager.Contest.CanJoin(contest_id, trackChanges: false);

            if (!can_attemp)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Contest has closed attemp");
            }

            _repositoryManager.JoinContest.Create(
                new JoinedToContest
                {
                    AccountId = account_id,
                    ContestId = contest_id
                });
            await _repositoryManager.SaveAsync();

            return Ok("Save change success");
        }

        /// <summary>
        /// Rate post of contest (Role: Manager, Member)
        /// </summary>
        /// <param name="post_of_contest_id"></param>
        /// <param name="parameters"></param>
        /// <param name="contest_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{contest_id}/rate/{post_of_contest_id}")]
        public async Task<IActionResult> RateTheContest(int post_of_contest_id, int contest_id, RateContestParameters parameters)
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
    }
}
