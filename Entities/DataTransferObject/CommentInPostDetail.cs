using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class CommentInPostDetail
    {
        public int Count { get; set; }
        public List<CommentReturn> Comments { get; set; }
    }
}
