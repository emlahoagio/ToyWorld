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

        /// <summary>
        /// Get feedback hasn't response yet
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("no_response_yet")]
        public async Task<IActionResult> GetNotReplyFeedback([FromQuery] PagingParameters paging)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            if (current_account.Role != 1) 
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to get");

            var feedbacks = await _repository.Feedback.GetFeedbacksNotReply(paging, trackChanges: false);

            return Ok(feedbacks);
        }

        /// <summary>
        /// Get feedback replied (Role: Manager)
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("replied")]
        public async Task<IActionResult> GetRepliedFeedback([FromQuery] PagingParameters paging)
        {
            var current_account = await _repository.Account.GetAccountById(_userAccessor.getAccountId(), trackChanges: false);

            if (current_account.Role != 1)
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission to get");

            var feedbacks = await _repository.Feedback.GetRepliedFeedback(paging, trackChanges: false);

            return Ok(feedbacks);
        }
    }
}
