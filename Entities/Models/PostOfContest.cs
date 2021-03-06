using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class PostOfContest
    {
        public PostOfContest()
        {
            Images = new HashSet<Image>();
            Rates = new HashSet<Rate>();
            Notifications = new HashSet<Notification>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public int ContestId { get; set; }
        public int AccountId { get; set; }
        public string Content { get; set; }
        public DateTime? DateCreate { get; set; }
        public int Status { get; set; }

        public virtual Contest Contest { get; set; } 
        public virtual Account Account { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
