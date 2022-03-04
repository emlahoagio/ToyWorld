using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class RateReturn
    {
        public int Id { get; set; }
        public int NumOfStar { get; set; }
        public string Note { get; set; }
        public int OwnerId { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerName { get; set; }
    }
}
