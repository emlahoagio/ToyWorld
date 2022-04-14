using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class CommentInPostDetail
    {
        public int Count { get; set; }
        public List<CommentReturn> Comments { get; set; }
    }
}
