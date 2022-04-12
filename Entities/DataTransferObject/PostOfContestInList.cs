using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class PostOfContestInList
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerName { get; set; }
        public int OwnerId { get; set; }
        public double AverageStar { get; set; }
        public bool IsRated { get; set; }
        public virtual List<RateReturn> Rates { get; set; }
        public virtual List<ImageReturn> Images { get; set; }
    }
}
