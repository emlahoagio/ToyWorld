using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Feedbacks = new HashSet<Feedback>();
            Images = new HashSet<Image>();
            ReactPosts = new HashSet<ReactPost>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime? PostDate { get; set; }
        public int? AccountId { get; set; }
        public int? GroupId { get; set; }
        public int? ToyId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Group Group { get; set; }
        public virtual Toy Toy { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ReactPost> ReactPosts { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
