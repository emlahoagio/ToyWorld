using System;

namespace ToyWorldSystem.Models
{
    public class ChatModel
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime When { get; set; }
    }
}
