using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IFeedbackRepository
    {
        void Create(Feedback feedback);
        Task<Pagination<NotReplyFeedback>> GetFeedbacksNotReply(PagingParameters paging, bool trackChanges);
        Task<Pagination<RepliedFeedback>> GetRepliedFeedback(PagingParameters paging, bool trackChanges);
        Task ReplyFeedback(int feedback_id, int replier_id, string reply_content, bool trackChanges);
        Task DeleteByPostId(int post_id, bool trackChanges);
    }
}
