using System;
using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class WaitingPost
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerName { get; set; }
        public string Content { get; set; }
        public DateTime? PostDate { get; set; }
        public List<ImageReturn> Images { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
