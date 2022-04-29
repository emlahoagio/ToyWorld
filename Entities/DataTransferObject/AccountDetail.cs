using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class AccountDetail
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public int NoOfPost { get; set; }
        public int NoOfFollowing { get; set; }
        public int NoOfFollower { get; set; }
        public bool IsFollowed { get; set; }
        public string Biography { get; set; }
        public string Name { get; set; }
        public List<WishList> WishLists { get; set; }
    }
}
