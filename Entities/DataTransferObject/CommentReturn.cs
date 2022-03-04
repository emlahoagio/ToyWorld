using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
