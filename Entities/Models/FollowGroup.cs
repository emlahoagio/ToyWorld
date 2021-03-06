#nullable disable

namespace Entities.Models
{
    public partial class FollowGroup
    {
        public int AccountId { get; set; }
        public int GroupId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Group Group { get; set; }
    }
}
