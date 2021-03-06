using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
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
        Task DeleteByPostOfContestId(int id, bool trackChanges);
        Task<Pagination<RepliedFeedback>> GetFeedbackByContent(int content, PagingParameters paging, bool trackChanges);
        Task<Feedback> GetById(int feedback_id, bool trackChanges);
    }
}
