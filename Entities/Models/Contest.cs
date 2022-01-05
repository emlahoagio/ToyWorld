using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Contest
    {
        public Contest()
        {
            Evaluates = new HashSet<Evaluate>();
            Images = new HashSet<Image>();
            PrizeContests = new HashSet<PrizeContest>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public int? MinRegistration { get; set; }
        public int? MaxRegistration { get; set; }
        public DateTime? StartRegistration { get; set; }
        public DateTime? EndRegistration { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? GroupId { get; set; }
        public int? ProposalId { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Proposal Proposal { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Evaluate> Evaluates { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<PrizeContest> PrizeContests { get; set; }
    }
}
