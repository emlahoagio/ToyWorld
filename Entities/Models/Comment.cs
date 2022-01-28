using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Comment
    {
        public Comment()
        {
            ReactComments = new HashSet<ReactComment>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public int? AccountId { get; set; }
        public int? PostId { get; set; }
        public int TradingPostId { get; set; }

        public virtual TradingPost TradingPost { get; set; }
        public virtual Account Account { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<ReactComment> ReactComments { get; set; }
    }
}
