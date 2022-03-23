using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class RepliedFeedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderAvatar { get; set; }
        public string FeedbackAbout { get; set; }
        public int? IdForDetail { get; set; }
        public DateTime SendDate { get; set; }
        public int? ReplierId { get; set; }
        public string ReplierName { get; set; }
        public string ReplierAvatar { get; set; }
        public string ReplyContent { get; set; }
    }
}
