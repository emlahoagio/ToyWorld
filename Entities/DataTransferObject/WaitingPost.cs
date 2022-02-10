using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
