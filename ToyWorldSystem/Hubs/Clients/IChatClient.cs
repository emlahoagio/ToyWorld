using System.Threading.Tasks;
using ToyWorldSystem.Models;

namespace ToyWorldSystem.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatModel message);
    }
}
