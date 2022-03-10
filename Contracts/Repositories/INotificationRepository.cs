using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface INotificationRepository
    {
        void CreateNotification(CreateNotificationModel model);
        void ChangeNotificationStatus(int id);
        IEnumerable<Notification> GetByAccountId(int accountId, bool track); //Realtime
    }
}
