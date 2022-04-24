using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface INotificationRepository
    {
        void CreateNotification(CreateNotificationModel model);
        Task<int> ChangeNotificationStatus(int id);
        Task<Pagination<Notification>> GetByAccountId(int accountId, PagingParameters paging); //Realtime
        Task Delete(int contest_id, bool trackChanges);
        Task DeleteByPostId(int post_id, bool trackChanges);
        Task DeleteByPostOfContestId(int id, bool trackChanges);
    }
}
