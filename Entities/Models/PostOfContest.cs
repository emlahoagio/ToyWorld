using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class PostOfContest
    {
        public PostOfContest()
        {
            Images = new HashSet<Image>();
            Rates = new HashSet<Rate>();
        }

        public int Id { get; set; }
        public int ContestId { get; set; }
        public int AccountId { get; set; }
        public string Content { get; set; }
        public DateTime? DateCreate { get; set; }
        public virtual Contest Contest { get; set; } 
        public virtual Account Account { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
