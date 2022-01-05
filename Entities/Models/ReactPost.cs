using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ReactPost
    {
        public int AccountId { get; set; }
        public int PostId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Post Post { get; set; }
    }
}
