using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class JoinedToContest
    {
        public int AccountId { get; set; }
        public int ContestId { get; set; }
        public bool IsBand { get; set; }

        public virtual Account Account { get; set; }
        public virtual Contest Contest { get; set; }
    }
}
