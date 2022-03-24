using System;

namespace Entities.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }

        public int SenderId { get; set; }
        public virtual Account Sender { get; set; }
    }
}
