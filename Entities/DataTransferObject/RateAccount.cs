using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class RateAccount
    {
        public double AverageStar { get; set; }
        public List<RateSellerReturn> Rates { get; set; }
    }
}
