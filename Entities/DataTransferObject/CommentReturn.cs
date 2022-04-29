using System;

namespace Entities.DataTransferObject
{
    public class CommentReturn
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAvatar { get; set; }
        public int NumOfReact { get; set; }
        public DateTime CommentDate { get; set; }
        public bool IsReacted { get; set; }
    }
}
