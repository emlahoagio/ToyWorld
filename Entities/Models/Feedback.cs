using System;

#nullable disable

namespace Entities.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? AccountId { get; set; }
        public int? TradingPostId { get; set; }
        public int? PostId { get; set; }
        public int? PostOfContestId { get; set; }
        public int? SenderId { get; set; }
        public string ReplyContent { get; set; }
        public int? AccountReplyId { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? ReplyDate { get; set; }

        public virtual Account Account { get; set; }
        public virtual Account AccountReply { get; set; }
        public virtual Post Post { get; set; }
        public virtual TradingPost TradingPost { get; set; }
        public virtual PostOfContest PostOfCotest { get; set; }
        public virtual Account Sender { get; set; }
    }
}
