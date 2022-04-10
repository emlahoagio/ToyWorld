using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class Rate
    {
        public int Id { get; set; }
        public double NumOfStar { get; set; }
        public string Note { get; set; }
        public int? AccountId { get; set; }
        public int PostOfContestId { get; set; }
        public virtual Account Account { get; set; }
        public virtual PostOfContest PostOfContest { get; set; }
    }
}
