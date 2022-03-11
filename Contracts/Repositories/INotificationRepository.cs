﻿using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface INotificationRepository
    {
        void CreateNotification(CreateNotificationModel model);
        Task ChangeNotificationStatus(int id);
        Task<Pagination<Notification>> GetByAccountId(int accountId, PagingParameters paging); //Realtime
    }
}
