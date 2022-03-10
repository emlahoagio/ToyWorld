using Entities.Models;
using Entities.RequestFeatures;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IChatRepository
    {
        void CreateChat(CreateChatModel model);
        void ChangeStatusChat(int id);
        IEnumerable<Chat> GetConversation(int senderId, int receiverId, bool track);
        IEnumerable<Chat> GetChatByReceiver(int receiverId, bool track); //realtime
    }
}
