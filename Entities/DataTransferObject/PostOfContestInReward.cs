using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class PostOfContestInReward
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerName { get; set; }
        public virtual List<ImageReturn> Images { get; set; }
        public int SumOfStart { get; set; }
    }
}
