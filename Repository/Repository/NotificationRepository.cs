using Contracts.Repositories;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Repository.Repository
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(DataContext context) : base(context)
        {

        }
        public void ChangeNotificationStatus(int id)
        {
            var noti = FindByCondition(x => x.Id == id, true).FirstOrDefaultAsync();
            noti.Result.IsReaded = true; //false read not yet, true read already.
        }

        public void CreateNotification(CreateNotificationModel model)
        {
            var noti = new Notification
            {
                Content = model.Content,
                CreateTime = DateTime.Now,
                IsReaded = false,
                AccountId = model.AccountId,
                ContestId = model.ContestId,
                PostId = model.PostId,
                PostOfContestId = model.PostOfContestId,
                TradingPostId = model.TradingPostId
            };
            Create(noti);
        }

        public IEnumerable<Notification> GetByAccountId(int accountId, bool track)
        {
            var listNoti = FindByCondition(x => x.AccountId == accountId, track).ToListAsync();
            return listNoti.Result;
        }
    }
}
