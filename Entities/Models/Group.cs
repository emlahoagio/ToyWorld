using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Group
    {
        public Group()
        {
            FollowGroups = new HashSet<FollowGroup>();
            ManageGroups = new HashSet<ManageGroup>();
            Posts = new HashSet<Post>();
            TradingPosts = new HashSet<TradingPost>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public bool IsDisable { get; set; }

        public virtual ICollection<FollowGroup> FollowGroups { get; set; }
        public virtual ICollection<ManageGroup> ManageGroups { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<TradingPost> TradingPosts { get; set; }
    }
}
