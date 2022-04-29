using System;

namespace Entities.DataTransferObject
{
    public class NotReplyFeedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderAvatar { get; set; }
        public string FeedbackAbout { get; set; }
        public int? IdForDetail { get; set; }
        public DateTime SendDate { get; set; }
    }
}
