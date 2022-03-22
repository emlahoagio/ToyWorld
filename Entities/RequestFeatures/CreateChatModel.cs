using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class CreateChatModel
    {
        public int UserId { get; set; }
        public string RoomName { get; set; }
        public string Content { get; set; }
    }
}
