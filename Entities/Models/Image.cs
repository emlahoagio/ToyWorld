﻿using System;
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
        public int? ContestId { get; set; }
        public int? PrizeId { get; set; }
        public int? ProposalId { get; set; }
        public int? ToyId { get; set; }

        public virtual Contest Contest { get; set; }
        public virtual Post Post { get; set; }
        public virtual Prize Prize { get; set; }
        public virtual Proposal Proposal { get; set; }
        public virtual Toy Toy { get; set; }
        public virtual TradingPost TradingPost { get; set; }
    }
}
