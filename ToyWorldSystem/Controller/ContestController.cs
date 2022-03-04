using Contracts;
using Entities.ErrorModel;
using Entities.Models;
using Entities.RequestFeatures;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

            return Ok(new {IsJoinedToContest = isInContest});
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
        /// Get information of proposal to create contest (Role: Manager)
        /// </summary>
        /// <param name="proposal_id"></param>
        /// <returns></returns>
        /// <exception cref="ErrorDetails"></exception>
        [HttpGet]
        [Route("create/proposal/{proposal_id}")]
        public async Task<IActionResult> GetProposalToCreateContest(int proposal_id)
        {
            var proposal_information = await _repositoryManager.Proposal.GetInformationToCreateContest(proposal_id, trackChanges: false);

            if (proposal_information == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No proposal matches with id: " + proposal_id);

            return Ok(proposal_information);
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
        public async Task<IActionResult> GetPostsOfContest(int contest_id, [FromQuery]PagingParameters paging)
        {
            var account_id = _userAccessor.getAccountId();

            var posts = await _repositoryManager.PostOfContest.GetPostOfContest(contest_id, paging, account_id, trackChanges: false);

            if (posts == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "This contest has no post");

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

            var rewards = await _repositoryManager.Image.GetImageForRewards(rewards_post_no_image, trackChanges: false);

            if (rewards == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "This contest has no reward");

            return Ok(rewards);
        }

        /// <summary>
        /// Get list subscribers of contest (Role: Manager)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{contest_id}/subscribers")]
        public async Task<IActionResult> GetListSubscribers(int contest_id, PagingParameters paging)
        {
            var subscribers = await _repositoryManager.JoinContest.GetListSubscribers(contest_id, paging, trackChanges: false);

            if (subscribers == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No subscribers in this contest");

            return Ok(subscribers);
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
            var joinContest = await _repositoryManager.JoinContest.GetSubsCriberToDelete(contest_id, account_id, trackChanges: false);

            _repositoryManager.JoinContest.Delete(joinContest);
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
            //get list prize descending by value
            var prizesList = await _repositoryManager.PrizeContest.GetPrizeForEndContest(contest_id, trackChanges: false);

            //Get list post of contest count star
            var postsOfContestList = await _repositoryManager.PostOfContest.GetPostOfContestForEndContest(contest_id, trackChanges: false);

            //For prize get highest star contest
            foreach(var prize in prizesList)
            {
                var post = postsOfContestList.First();
                if(post != null)
                {
                    _repositoryManager.Reward.Create(new Reward
                    {
                        AccountId = post.AccountId,
                        ContestId = contest_id,
                        PostOfContestId = post.Id,
                        PrizeId = prize.Id
                    });
                }else
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

            var postOfContest = new PostOfContest
            {
                AccountId = accountId,
                Content = param.Content,
                ContestId = contest_id,
                Images = param.ImagesUrl.Select(x => new Image { Url = x }).ToList(),
                DateCreate = DateTime.Now
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

            var brand = await _repositoryManager.Brand
                .GetBrandByName(param.BrandName == null ? "Unknow Brand" : param.BrandName, trackChanges: false);
            if (brand == null)
            {
                _repositoryManager.Brand.CreateBrand(new Brand { Name = param.BrandName });
                await _repositoryManager.SaveAsync();
                brand = await _repositoryManager.Brand.GetBrandByName(param.BrandName, trackChanges: false);
            }

            var type = await _repositoryManager.Type
                .GetTypeByName(param.TypeName == null ? "Unknow Type" : param.TypeName, trackChanges: false);
            if (type == null)
            {
                _repositoryManager.Type.CreateType(new Entities.Models.Type { Name = param.TypeName });
                await _repositoryManager.SaveAsync();
                type = await _repositoryManager.Type.GetTypeByName(param.BrandName, trackChanges: false);
            }

            var contest = new Contest
            {
                Title = param.Title,
                Description = param.Description,
                Venue = param.Location,
                CoverImage = param.CoverImage,
                Slogan = param.Slogan,
                IsOnlineContest = param.IsOnlineContest,
                RegisterCost = param.RegisterCost,
                MinRegistration = param.MinRegistration,
                MaxRegistration = param.MaxRegistration,
                StartRegistration = param.StartRegistration,
                EndRegistration = param.EndRegistration,
                StartDate = param.StartDate,
                EndDate = param.EndDate,
                GroupId = group_id,
                ProposalId = param.ProposalId,
                BrandId = brand.Id,
                TypeId = type.Id,
                CanAttempt = false,
                Status = 0
            };

            if(contest.StartRegistration.Value.Day == DateTime.Now.Day)
            {
                contest.CanAttempt = true;
                contest.Status = 1;
            }

            _repositoryManager.Contest.Create(contest);
            await _repositoryManager.SaveAsync();

            var createdContest = await _repositoryManager.Contest.GetCreatedContest(group_id, param.Title, param.StartRegistration, trackChanges: false);

            //schedule for contest
            if (contest.StartRegistration.Value.Day != DateTime.Now.Day)
            {
                startRegis = contest.StartRegistration.Value;
               BackgroundJob.Schedule(() => StartRegisContest(createdContest.Id), new DateTime(startRegis.Year, startRegis.Month, startRegis.Day, 0, 0, 1, DateTimeKind.Local));
            }

            endRegis = contest.EndRegistration.Value;
            BackgroundJob.Schedule(() => ClosedRegisContest(createdContest.Id), new DateTime(endRegis.Year, endRegis.Month, endRegis.Day, 23, 59, 59, DateTimeKind.Local));
            startDate = contest.StartDate.Value;
            BackgroundJob.Schedule(() => OpenContest(createdContest.Id), new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 1, DateTimeKind.Local));
            endDate = contest.EndDate.Value;
            BackgroundJob.Schedule(() => ClosedContest(createdContest.Id), new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, DateTimeKind.Local));

            return Ok(new { contestId = createdContest.Id });
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
        /// <returns></returns>
        [HttpPost]
        [Route("rate/{post_of_contest_id}")]
        public async Task<IActionResult> RateTheContest(int post_of_contest_id, RateContestParameters parameters)
        {
            var account_id = _userAccessor.getAccountId();

            var rate = new Rate
            {
                AccountId = account_id,
                Note = parameters.Note,
                NumOfStart = parameters.NumOfStart,
                PostOfContestId = post_of_contest_id
            };

            _repositoryManager.Rate.Create(rate);
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
