using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
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

        #region Create notification
        /// <summary>
        /// Create notification
        /// </summary>
        /// <param name="notificationModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationModel notificationModel)
        {
            _repositoryManager.Notification.CreateNotification(notificationModel);
            await _repositoryManager.SaveAsync();
            return Ok("Success");
        }
        #endregion

        #region Readed notification
        /// <summary>
        /// Change status to readed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("readed")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _repositoryManager.Notification.ChangeNotificationStatus(id);
            if (result == 1)
            {
                await _repositoryManager.SaveAsync();
                return Ok("Success");
            }
            else return Ok("This notification doesn't exist!");
        }
        #endregion

        #region Get notification
        /// <summary>
        /// Get list
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetNotificationByOwnerId(int ownerId, [FromQuery] PagingParameters paging)
        {

            var result = await _repositoryManager.Notification.GetByAccountId(ownerId, paging);
            //Thread.Sleep(5000);
            return Ok(result);
        }
        #endregion
    }
}
