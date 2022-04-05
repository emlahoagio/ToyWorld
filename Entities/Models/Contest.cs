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
            PrizeContests = new HashSet<PrizeContest>();
            Posts = new HashSet<PostOfContest>();
            AccountJoined = new HashSet<JoinedToContest>();
            Rewards = new HashSet<Reward>();
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public string Slogan { get; set; }
        public int? MinRegistration { get; set; }
        public int? MaxRegistration { get; set; }
        public DateTime? StartRegistration { get; set; }
        public DateTime? EndRegistration { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? GroupId { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int? Status { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Evaluate> Evaluates { get; set; }
        public virtual ICollection<PrizeContest> PrizeContests { get; set; }
        public virtual ICollection<PostOfContest> Posts { get; set; }
        public virtual ICollection<JoinedToContest> AccountJoined { get; set; }
        public virtual ICollection<Reward> Rewards { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
