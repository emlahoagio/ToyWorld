using Microsoft.AspNetCore.SignalR;
using ToyWorldSystem.Hubs;

namespace Message.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
    }
}
