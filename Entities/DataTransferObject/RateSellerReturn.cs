using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class RateSellerReturn
    {
        public int RateOwnerId { get; set; }
        public string RateOwnerName { get; set; }
        public string RateOwnerAvatar { get; set; }
        public double NumOfStar { get; set; }
        public string Content { get; set; }
        public string TradingPostTitle { get; set; }
        public int TradingPostId { get; set; }
    }
}
