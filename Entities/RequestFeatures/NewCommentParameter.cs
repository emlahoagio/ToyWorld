using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class NewCommentParameter
    {
        public int AccountId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
    }
}
