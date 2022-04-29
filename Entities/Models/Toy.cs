using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Toy
    {
        public Toy()
        {
            Images = new HashSet<Image>();
            Posts = new HashSet<Post>();
            TradingPosts = new HashSet<TradingPost>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<TradingPost> TradingPosts { get; set; }
    }
}
