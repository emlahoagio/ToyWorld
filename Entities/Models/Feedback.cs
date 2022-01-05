using System;
using System.Collections.Generic;

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
        public int? SenderId { get; set; }
        public string ReplyContent { get; set; }
        public int? AccountReplyId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Account AccountReply { get; set; }
        public virtual Post Post { get; set; }
        public virtual Account Sender { get; set; }
    }
}
