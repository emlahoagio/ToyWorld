using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class TradingPost
    {
        public TradingPost()
        {
            Images = new HashSet<Image>();
            ReactTradingPosts = new HashSet<ReactTradingPost>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ToyName { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string Trading { get; set; }
        public decimal? Value { get; set; }
        public string Phone { get; set; }
        public int GroupId { get; set; }
        public int? AccountId { get; set; }
        public int? ToyId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Toy Toy { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ReactTradingPost> ReactTradingPosts { get; set; }
    }
}
