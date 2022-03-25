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
            chat.IsRead = true;
        }

        public void CreateChat(CreateChatModel model)
        {
            var chat = new Chat
            {
                AccountId = model.UserId,
                Content = model.Content,
                RoomName = model.RoomName,
                SendDate = DateTime.Now,
                IsRead = false
            };
            Create(chat);
        }

        public async Task<Pagination<Chat>> GetConversation(string roomName, PagingParameters paging)
        {
            var conversation = await FindByCondition(x => x.RoomName == roomName, false)
                .OrderByDescending(x => x.SendDate)
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

        public async Task<IEnumerable<string>> GetListRoomByUserId(int userId)
        {
            var listChat = await FindAll(false).ToListAsync();
            var listRoom = new List<string>() ; 
            foreach (var chat in listChat)
            {
                string []userid = chat.RoomName.Split("-");
                if (userid[0] == userId.ToString() || userid[1] == userId.ToString())
                {
                    listRoom.Add(chat.RoomName);
                }
            }
            return listRoom;
        }
    }
}
