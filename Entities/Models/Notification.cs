using System;

namespace Entities.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsReaded { get; set; }
        public int? AccountId { get; set; }
        public int? PostId { get; set; }
        public int? TradingPostId { get; set; }
        public int? PostOfContestId { get; set; }
        public int? ContestId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Post Post { get; set; }
        public virtual TradingPost TradingPost { get; set; }
        public virtual PostOfContest PostOfContest { get; set; }
        public virtual Contest Contest { get; set; }
    }
}
