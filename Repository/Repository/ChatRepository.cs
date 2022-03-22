using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(DataContext context) : base(context)
        {

        }
        public async Task ChangeStatusChat(int id)
        {
            var chat = await FindByCondition(x => x.Id == id, false).FirstOrDefaultAsync();
            chat.IsRead = true;
        }

        public void CreateChat(CreateChatModel model)
        {
            var chat = new Chat
            {
                AccountId = model.UserId,
                Content = model.Content,
                RoomName = model.RoomName,
                When = DateTime.Now,
                IsRead = false
            };
            Create(chat);
        }

        public Task<Pagination<Chat>> GetChatByReceiver(int receiverId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<Chat>> GetConversation(string roomName, PagingParameters paging)
        {
            var conversation = await FindByCondition(x => x.RoomName == roomName, false)
                .OrderByDescending(x => x.When)
                .ToListAsync();

            var subConversation = conversation.Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            return new Pagination<Chat>
            {
                Data = subConversation,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };
        }
    }
}
