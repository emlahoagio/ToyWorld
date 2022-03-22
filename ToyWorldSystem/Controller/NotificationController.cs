using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        public NotificationController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationModel notificationModel)
        {
            _repositoryManager.Notification.CreateNotification(notificationModel);
            await _repositoryManager.SaveAsync();
            return Ok("Success");
        }

        [Route("changestatus")]
        [HttpPut]
        public async Task<IActionResult> Update(int id)
        {
            await _repositoryManager.Notification.ChangeNotificationStatus(id);
            await _repositoryManager.SaveAsync();
            return Ok("Success");
        }

        [Route("getnotification")]
        [HttpGet]
        public async Task<IActionResult> GetNotificationByOwnerId(int ownerId, [FromQuery] PagingParameters paging)
        {

            var result = await _repositoryManager.Notification.GetByAccountId(ownerId, paging);
            //Thread.Sleep(5000);
            return Ok(result);

        }
    }
}
