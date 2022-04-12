using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(DataContext context) : base(context)
        {

        }
        public async Task ChangeNotificationStatus(int id)
        {
            var noti = await FindByCondition(x => x.Id == id, true).FirstOrDefaultAsync();
            noti.IsReaded = true; //false read not yet, true read already.
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

        public async Task Delete(int contest_id, bool trackChanges)
        {
            var notifications = await FindByCondition(x => x.ContestId == contest_id, trackChanges).ToListAsync();

            foreach(var notification in notifications)
            {
                Delete(notification);
            }
        }

        public async Task DeleteByPostId(int post_id, bool trackChanges)
        {
            var notifications = await FindByCondition(x => x.PostId == post_id, trackChanges).ToListAsync();

            if(notifications.Count != 0)
            {
                foreach(var notification in notifications)
                {
                    Delete(notification);
                }
            }
        }

        public async Task DeleteByPostOfContestId(int id, bool trackChanges)
        {
            var notifications = await FindByCondition(x => x.PostOfContestId == id, trackChanges).ToListAsync();

            if (notifications.Count != 0)
            {
                foreach (var notification in notifications)
                {
                    Delete(notification);
                }
            }
        }

        public async Task<Pagination<Notification>> GetByAccountId(int accountId, PagingParameters paging)
        {
            var notifies = await FindByCondition(x => x.AccountId == accountId, false)
                //.Include(x => x.Account)
                //.Include(x => x.TradingPost)
                //.Include(x => x.Post)
                //.Include(x => x.PostOfContest)
                //.Include(x => x.Contest)
                .OrderByDescending(x => x.CreateTime)
                .ToListAsync();

            var subNoti = notifies.Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            var result = new Pagination<Notification>
            {
                Data = subNoti,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };
            return result;
        }
    }
}
