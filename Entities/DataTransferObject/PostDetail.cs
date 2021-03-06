using System;
using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class PostDetail
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerName { get; set; }
        public string Content { get; set; }
        public DateTime? PostDate { get; set; }
        public int NumOfReact { get; set; }
        public bool IsLikedPost { get; set; }
        public List<ImageReturn> Images { get; set; }
        public List<CommentReturn> Comments { get; set; }
        public int NumOfComment { get; set; }
    }
}
