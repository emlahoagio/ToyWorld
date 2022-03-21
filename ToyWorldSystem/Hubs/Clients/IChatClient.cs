using System.Threading.Tasks;
using ToyWorldSystem.Models;

namespace ToyWorldSystem.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatModel message);
    }
}
