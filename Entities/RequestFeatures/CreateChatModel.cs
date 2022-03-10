using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class CreateChatModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
