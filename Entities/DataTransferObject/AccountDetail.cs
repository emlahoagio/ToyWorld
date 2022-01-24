using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class AccountDetail
    {
        public string Avatar { get; set; }
        public int NoOfPost { get; set; }
        public int NoOfFollowing { get; set; }
        public int NoOfFollower { get; set; }
        public string Biography { get; set; }
        public string Name { get; set; }
    }
}
