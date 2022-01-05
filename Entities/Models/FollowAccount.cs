using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class FollowAccount
    {
        public int AccountId { get; set; }
        public int AccountFollowId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Account AccountFollow { get; set; }
    }
}
