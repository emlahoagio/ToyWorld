using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            chat.IsReaded = true;
        }

        public void CreateChat(CreateChatModel model)
        {
            var chat = new Chat
            {
                SenderId = model.SenderId,
                ReceiverId = model.ReceiverId,
                Content = model.Content,
                When = DateTime.Now,
                IsReaded = false
            };
            Create(chat);
        }

        public Task<Pagination<Chat>> GetChatByReceiver(int receiverId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<Chat>> GetConversation(int senderId, int receiverId, PagingParameters paging)
        {
            var conversation = await FindByCondition(x => x.SenderId == senderId && x.ReceiverId == receiverId || x.SenderId == receiverId && x.ReceiverId == senderId, false)
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
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
