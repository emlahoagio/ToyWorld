using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Prize
    {
        public Prize()
        {
            Images = new HashSet<Image>();
            PrizeContests = new HashSet<PrizeContest>();
            ProposalPrizes = new HashSet<ProposalPrize>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<PrizeContest> PrizeContests { get; set; }
        public virtual ICollection<ProposalPrize> ProposalPrizes { get; set; }
    }
}
