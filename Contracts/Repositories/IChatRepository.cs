using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IChatRepository
    {
        void CreateChat(CreateChatModel model);
        Task ChangeStatusChat(int id);
        Task<Pagination<Chat>> GetConversation(string roomName, PagingParameters paging);
        Task<Pagination<Chat>> GetChatByReceiver(int receiverId); //realtime
    }
}
