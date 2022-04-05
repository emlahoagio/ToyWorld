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
            Rewards = new HashSet<Reward>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool IsDisabled { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<PrizeContest> PrizeContests { get; set; }
        public virtual ICollection<Reward> Rewards { get; set; }
    }
}
