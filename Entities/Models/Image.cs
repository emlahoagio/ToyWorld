using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? PostId { get; set; }
        public int? TradingPostId { get; set; }
        public int? PrizeId { get; set; }
        public int? ToyId { get; set; }
        public int? PostOfContestId { get; set; }
        public int? BillId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Prize Prize { get; set; }
        public virtual Toy Toy { get; set; }
        public virtual TradingPost TradingPost { get; set; }
        public virtual PostOfContest PostOfContest { get; set; }
        public virtual Bill Bill { get; set; }
    }
}
