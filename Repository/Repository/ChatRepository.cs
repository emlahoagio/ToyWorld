using Contracts.Repositories;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Repository.Repository
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(DataContext context) : base(context)
        {

        }
        public void ChangeStatusChat(int id)
        {
            var chat = FindByCondition(x => x.Id == id, false).FirstOrDefaultAsync();
            chat.Result.IsReaded = true;
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

        public IEnumerable<Chat> GetChatByReceiver(int receiverId, bool track)
        {
            var result = FindByCondition(x => x.ReceiverId == receiverId, track).ToListAsync();
            return result.Result;
        }

        public IEnumerable<Chat> GetConversation(int senderId, int receiverId, bool track)
        {
            var conversation = FindByCondition(x => x.ReceiverId == receiverId && x.SenderId == senderId, track).ToListAsync();
            return conversation.Result;
        }
    }
}
