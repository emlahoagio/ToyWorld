using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        public ChatController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [AllowAnonymous]
        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateChatModel model)
        {
            _repositoryManager.Chat.CreateChat(model);
            await _repositoryManager.SaveAsync();
            return Ok("Created");
        }

        [AllowAnonymous]
        [Route("changestatus")]
        [HttpPut]
        public async Task<IActionResult> Update(int id)
        {
            await _repositoryManager.Chat.ChangeStatusChat(id);
            await _repositoryManager.SaveAsync();
            return Ok("Updated");
        }

        [AllowAnonymous]
        [Route("getconversation")]
        [HttpGet]
        public async Task<IActionResult> GetConversation(int senderId, int receiverId, [FromQuery] PagingParameters paging)
        {
            var result = await _repositoryManager.Chat.GetConversation(senderId, receiverId, paging);
            return Ok(result);
        }
    }
}
