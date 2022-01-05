using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class ReactComment
    {
        public int AccountId { get; set; }
        public int CommentId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
