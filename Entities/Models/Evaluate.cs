using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Evaluate
    {
        public int ContestId { get; set; }
        public int AccountId { get; set; }
        public int? NoOfStart { get; set; }
        public string Comment { get; set; }

        public virtual Account Account { get; set; }
        public virtual Contest Contest { get; set; }
    }
}
