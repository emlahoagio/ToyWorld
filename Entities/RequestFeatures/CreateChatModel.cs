namespace Entities.RequestFeatures
{
    public class CreateChatModel
    {
        public int UserId { get; set; }
        public string RoomName { get; set; }
        public string Content { get; set; }
    }
}
