using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class CreateNotificationModel
    {
        public string Content { get; set; }
        public int? AccountId { get; set; }
        public int? PostId { get; set; }
        public int? TradingPostId { get; set; }
        public int? PostOfContestId { get; set; }
        public int? ContestId { get; set; }
    }
}
