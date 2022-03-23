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
    }
}
