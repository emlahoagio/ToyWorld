using Contracts;
using Entities.ErrorModel;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/feedbacks")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public FeedbackController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        #region Get not response feedback
        /// <summary>
        /// Get feedback hasn't response yet
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("no_response_yet")]
        public async Task<IActionResult> GetNotReplyFeedback([FromQuery] PagingParameters paging)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);

            if (current_account.Role != 1) 
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to get");

            var feedbacks = await _repository.Feedback.GetFeedbacksNotReply(paging, trackChanges: false);

            return Ok(feedbacks);
        }
        #endregion

        #region Get replied feedback
        /// <summary>
        /// Get feedback replied (Role: Manager)
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("replied")]
        public async Task<IActionResult> GetRepliedFeedback([FromQuery] PagingParameters paging)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);

            if (current_account.Role != 1)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to get");

            var feedbacks = await _repository.Feedback.GetRepliedFeedback(paging, trackChanges: false);

            return Ok(feedbacks);
        }
        #endregion

        #region Get feed back by content
        /// <summary>
        /// Get feedback by content
        /// </summary>
        /// <param name="content">0: account, 1: trading, 2: post, 3: post of contest</param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("content/{content}")]
        public async Task<IActionResult> GetFeedbackByContent(int content, [FromQuery]PagingParameters paging)
        {
            var feedbacks = await _repository.Feedback.GetFeedbackByContent(content, paging, trackChanges: false);

            if (feedbacks == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No feedback with this content");

            return Ok(feedbacks);
        }
        #endregion

        #region Reply feedback
        /// <summary>
        /// Reply feedback, Update reply of feedback (Role: Manager)
        /// </summary>
        /// <param name="feedback_id"></param>
        /// <param name="replyContent"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{feedback_id}/reply")]
        public async Task<IActionResult> ReplyFeedback(int feedback_id, string replyContent)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.GetAccountId(), trackChanges: false);

            if (current_account.Role != 1)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to reply");

            await _repository.Feedback.ReplyFeedback(feedback_id, current_account.Id, replyContent, trackChanges: false);

            //Push notification (To sender of feedback)

            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion
    }
}
