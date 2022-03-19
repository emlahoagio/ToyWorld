using System;

namespace Entities.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime When { get; set; }
        public bool IsReaded { get; set; }

        public virtual Account Sender { get; set; }
        public virtual Account Receiver { get; set; }
    }
}
