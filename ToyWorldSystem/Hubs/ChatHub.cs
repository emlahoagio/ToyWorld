using Microsoft.AspNetCore.SignalR;
using ToyWorldSystem.Hubs.Clients;

namespace ToyWorldSystem.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
    }
}
