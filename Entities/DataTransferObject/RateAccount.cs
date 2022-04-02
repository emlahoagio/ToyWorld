using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class RateAccount
    {
        public double AverageStar { get; set; }
        public List<RateSellerReturn> Rates { get; set; }
    }
}
