using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Type
    {
        public Type()
        {
            Contests = new HashSet<Contest>();
            Toys = new HashSet<Toy>();
            TradingPosts = new HashSet<TradingPost>();
            FavoriteTypes = new HashSet<FavoriteType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TradingPost> TradingPosts { get; set; }
        public virtual ICollection<Contest> Contests { get; set; }
        public virtual ICollection<Toy> Toys { get; set; }
        public virtual ICollection<FavoriteType> FavoriteTypes { get; set; }
    }
}
