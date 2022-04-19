using Contracts;
using Entities.ErrorModel;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/submissions")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public SubmissionController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        #region Get by contest id
        /// <summary>
        /// Get submission in contest to managed (Role: Manager)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("contest/{contest_id}")]
        public async Task<IActionResult> GetSubmissionByContestId(int contest_id, [FromQuery] PagingParameters paging)
        {
            var account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);
            if (account.Role != 1)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to get");

            var posts = await _repository.PostOfContest.GetPostByContestId(contest_id, paging, trackChanges: false);

            return Ok(posts);
        }
        #endregion

        #region Get by contest id mobile
        /// <summary>
        /// Get submission in contest to managed mobile (Role: Manager)
        /// </summary>
        /// <param name="contest_id"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("contest/{contest_id}/mobile")]
        public async Task<IActionResult> GetSubmissionByContestIdMb(int contest_id, [FromQuery] PagingParameters paging)
        {
            var account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);
            if (account.Role != 1)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to get");

            var posts = await _repository.PostOfContest.GetPostByContestId(contest_id, paging, trackChanges: false);

            posts = await _repository.Image.GetImageForPostOfContest(posts, trackChanges: false);

            return Ok(posts);
        }
        #endregion

        #region Get top 10
        [HttpGet]
        [Route("{contest_id}/top10")]
        public async Task<IActionResult> GetTop10Submission(int contest_id)
        {
            var submissionsId = await _repository.PostOfContest.GetIdOfPost(contest_id, trackChanges: false);

            if (submissionsId.Count == 0) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No post in this contest");

            var rewards = await _repository.Reward.GetIdOfPostHasReward(contest_id, trackChanges: false);

            var idsPostInTop = await _repository.Rate.GetIdOfPostInTop10(submissionsId, rewards, trackChanges: false);

            var posts = await _repository.PostOfContest.GetPostOfContestById(idsPostInTop, trackchanges: false);
            posts = await _repository.Rate.GetRateForPostOfContest(posts, trackChanges: false);
            posts = await _repository.Image.GetImageForPostOfContest(posts, trackChanges: false);

            return Ok(posts);
        }
        #endregion

        #region Approve/deny submission
        /// <summary>
        /// Approve or deny submission
        /// </summary>
        /// <param name="approve_or_deny">1: approve, 0: deny</param>
        /// <param name="post_of_contest_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{post_of_contest_id}/{approve_or_deny}")]
        public async Task<IActionResult> ApproveOrDenyPost(int approve_or_deny, int post_of_contest_id)
        {
            var account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);
            if (account.Role != 1)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to update");

            var post = await _repository.PostOfContest.GetById(post_of_contest_id, trackChanges: false);

            if (post == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "Not found to update");

            if (approve_or_deny == 1)
            {
                _repository.PostOfContest.Approve(post);
            }
            else if (approve_or_deny == 0)
            {
                _repository.PostOfContest.Deny(post);
            }
            else throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid status to change");

            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion

        #region Add reward for submission
        /// <summary>
        /// Add reward for contest
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("reward")]
        public async Task<IActionResult> AddRewardForSubmission(NewReward reward)
        {
            var account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);
            if (account.Role != 1)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to update");

            var post = await _repository.PostOfContest.GetById(reward.PostOfContestId, trackChanges: false);

            var newReward = new Reward
            {
                PostOfContestId = post.Id,
                ContestId = post.ContestId,
                AccountId = post.AccountId,
                PrizeId = reward.PrizeId
            };

            _repository.Reward.Create(newReward);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion
    }
}
