using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class Reward
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int PostOfContestId { get; set; }
        public int ContestId { get; set; }
        public int PrizeId { get; set; }

        public virtual PostOfContest PostOfContest { get; set; }
        public virtual Account Account { get; set; }
        public virtual Contest Contest { get; set; }
        public virtual Prize Prize { get; set; }

    }
}
