using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class PostOfContestInReward
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerName { get; set; }
        public virtual List<ImageReturn> Images { get; set; }
        public double SumOfStart { get; set; }
    }
}
