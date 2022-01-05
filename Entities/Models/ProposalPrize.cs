using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ProposalPrize
    {
        public int ProposalId { get; set; }
        public int PrizeId { get; set; }

        public virtual Prize Prize { get; set; }
        public virtual Proposal Proposal { get; set; }
    }
}
