using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(DataContext context) : base(context)
        {
        }

        public async Task<Pagination<NotReplyFeedback>> GetFeedbacksNotReply(PagingParameters paging, bool trackChanges)
        {
            var feedback = await FindByCondition(x => x.ReplyDate == null, trackChanges)
                .Include(x => x.PostOfCotest)
                .Include(x => x.Sender)
                .Include(x => x.TradingPost)
                .Include(x => x.Post)
                .Include(x => x.Account)
                .OrderByDescending(x => x.SendDate)
                .ToListAsync();

            var count = feedback.Count;

            var paging_feedback = feedback.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            var list_result = paging_feedback.Select(x => new NotReplyFeedback
            {
                Content = x.Content,
                Id = x.Id,
                SenderAvatar = x.Sender.Avatar,
                SenderId = x.SenderId,
                SenderName = x.Sender.Name,
                SendDate = x.SendDate,
                FeedbackAbout = x.TradingPost != null ? "trading" : x.Account != null ? "account" : x.Post != null ? "post" : "post_of_contest",
                IdForDetail = x.TradingPost != null ? x.TradingPostId : x.Account != null ? x.AccountId : x.Post != null ? x.PostId : x.PostOfContestId
            });

            var result = new Pagination<NotReplyFeedback>
            {
                Count = count,
                Data = list_result,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };
            return result;
        }

        public async Task<Pagination<RepliedFeedback>> GetRepliedFeedback(PagingParameters paging, bool trackChanges)
        {
            var feedback = await FindByCondition(x => x.ReplyDate == null, trackChanges)
                .Include(x => x.PostOfCotest)
                .Include(x => x.Sender)
                .Include(x => x.TradingPost)
                .Include(x => x.Post)
                .Include(x => x.Account)
                .Include(x => x.AccountReply)
                .OrderByDescending(x => x.SendDate)
                .ToListAsync();

            var count = feedback.Count;

            var paging_feedback = feedback.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            var list_result = paging_feedback.Select(x => new RepliedFeedback
            {
                Content = x.Content,
                Id = x.Id,
                SenderAvatar = x.Sender.Avatar,
                SenderId = x.SenderId,
                SenderName = x.Sender.Name,
                SendDate = x.SendDate,
                FeedbackAbout = x.TradingPost != null ? "trading" : x.Account != null ? "account" : x.Post != null ? "post" : "post_of_contest",
                IdForDetail = x.TradingPost != null ? x.TradingPostId : x.Account != null ? x.AccountId : x.Post != null ? x.PostId : x.PostOfContestId,
                ReplierAvatar = x.AccountReply.Avatar,
                ReplierId = x.AccountReplyId,
                ReplyContent = x.ReplyContent,
                ReplierName = x.AccountReply.Name
            });

            var result = new Pagination<RepliedFeedback>
            {
                Count = count,
                Data = list_result,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };
            return result;
        }
    }
}
