using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(DataContext context) : base(context)
        {
        }

        public async Task DeleteByPostId(int post_id, bool trackChanges)
        {
            var feedbacks = await FindByCondition(x => x.PostId == post_id, trackChanges).ToListAsync();

            if (feedbacks.Count != 0)
            {
                foreach (var feedback in feedbacks)
                {
                    Delete(feedback);
                }
            }
        }

        public async Task DeleteByPostOfContestId(int id, bool trackChanges)
        {
            var feedbacks = await FindByCondition(x => x.PostOfContestId == id, trackChanges).ToListAsync();

            if (feedbacks.Count != 0)
            {
                foreach (var feedback in feedbacks)
                {
                    Delete(feedback);
                }
            }
        }

        public async Task<Pagination<RepliedFeedback>> GetFeedbackByContent(int content, PagingParameters paging, bool trackChanges)
        {
            var query = new List<Feedback>();
            int count = 0;

            switch (content)
            {
                case 0:
                    {
                        query = await FindByCondition(x => x.AccountId != null, trackChanges)
                            .OrderByDescending(x => x.Id)
                            .Skip((paging.PageNumber - 1) * paging.PageSize)
                            .Take(paging.PageSize)
                            .Include(x => x.Sender)
                            .Include(x => x.AccountReply)
                            .ToListAsync();
                        count = await FindByCondition(x => x.AccountId != null, trackChanges).CountAsync();
                    }
                    break;
                case 1:
                    {
                        query = await FindByCondition(x => x.TradingPostId != null, trackChanges)
                            .OrderByDescending(x => x.Id)
                            .Skip((paging.PageNumber - 1) * paging.PageSize)
                            .Take(paging.PageSize)
                            .Include(x => x.Sender)
                            .Include(x => x.AccountReply)
                            .ToListAsync();
                        count = await FindByCondition(x => x.TradingPostId != null, trackChanges).CountAsync();
                    }
                    break;
                case 2:
                    {
                        query = await FindByCondition(x => x.PostId != null, trackChanges)
                            .OrderByDescending(x => x.Id)
                            .Skip((paging.PageNumber - 1) * paging.PageSize)
                            .Take(paging.PageSize)
                            .Include(x => x.Sender)
                            .Include(x => x.AccountReply)
                            .ToListAsync();
                        count = await FindByCondition(x => x.PostId != null, trackChanges).CountAsync();
                    }
                    break;
                case 3:
                    {
                        query = await FindByCondition(x => x.PostOfContestId != null, trackChanges)
                            .OrderByDescending(x => x.Id)
                            .Skip((paging.PageNumber - 1) * paging.PageSize)
                            .Take(paging.PageSize)
                            .Include(x => x.Sender)
                            .Include(x => x.AccountReply)
                            .ToListAsync();
                        count = await FindByCondition(x => x.PostOfContestId != null, trackChanges).CountAsync();
                    }
                    break;
                default: return null;
            }

            var result = new Pagination<RepliedFeedback>
            {
                Count = count,
                Data = query.Select(x => new RepliedFeedback
                {
                    Content = x.Content,
                    Id = x.Id,
                    SenderAvatar = x.Sender.Avatar,
                    SenderId = x.SenderId,
                    SenderName = x.Sender.Name,
                    SendDate = x.SendDate,
                    FeedbackAbout = x.TradingPostId != null ? "trading" : x.AccountId != null ? "account" : x.PostId != null ? "post" : "post_of_contest",
                    IdForDetail = x.TradingPostId != null ? x.TradingPostId : x.AccountId != null ? x.AccountId : x.PostId != null ? x.PostId : x.PostOfContestId,
                    ReplierAvatar = x.AccountReply != null ? x.AccountReply.Avatar : "",
                    ReplierId = x.AccountReplyId,
                    ReplyContent = x.ReplyContent,
                    ReplierName = x.AccountReply != null ? x.AccountReply.Name : ""
                }).ToList(),
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }

        public async Task<Pagination<NotReplyFeedback>> GetFeedbacksNotReply(PagingParameters paging, bool trackChanges)
        {
            var feedback = await FindByCondition(x => x.ReplyDate == null, trackChanges)
                .Include(x => x.Sender)
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
                FeedbackAbout = x.TradingPostId != null ? "trading" : x.AccountId != null ? "account" : x.PostId != null ? "post" : "post_of_contest",
                IdForDetail = x.TradingPostId != null ? x.TradingPostId : x.AccountId != null ? x.AccountId : x.PostId != null ? x.PostId : x.PostOfContestId
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
            var feedback = await FindByCondition(x => x.ReplyDate != null, trackChanges)
                .Include(x => x.Sender)
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
                FeedbackAbout = x.TradingPostId != null ? "trading" : x.AccountId != null ? "account" : x.PostId != null ? "post" : "post_of_contest",
                IdForDetail = x.TradingPostId != null ? x.TradingPostId : x.AccountId != null ? x.AccountId : x.PostId != null ? x.PostId : x.PostOfContestId,
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

        public async Task ReplyFeedback(int feedback_id, int replier_id, string reply_content, bool trackChanges)
        {
            var feedback = await FindByCondition(x => x.Id == feedback_id, trackChanges).FirstOrDefaultAsync();

            feedback.ReplyContent = reply_content;
            feedback.ReplyDate = DateTime.UtcNow;
            feedback.AccountReplyId = replier_id;

            Update(feedback);
        }
    }
}
