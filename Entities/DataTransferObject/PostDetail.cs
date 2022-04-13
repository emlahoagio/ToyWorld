using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class PostDetail
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerName { get; set; }
        public string Content { get; set; }
        public DateTime? PublicDate { get; set; }
        public int NumOfReact { get; set; }
        public bool IsLikedPost { get; set; }
    }
}
