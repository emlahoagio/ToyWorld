﻿using Contracts;
using Contracts.Repositories;
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

        [AllowAnonymous]
        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationModel notificationModel)
        {
            _repositoryManager.Notification.CreateNotification(notificationModel);
            await _repositoryManager.SaveAsync();
            return Ok("Success");
        }

        [AllowAnonymous]
        [Route("changestatus")]
        [HttpPut]
        public async Task<IActionResult> Update(int id)
        {
            _repositoryManager.Notification.ChangeNotificationStatus(id);
            await _repositoryManager.SaveAsync();
            return Ok("Success");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetNotificationByOwnerId(int ownerId, bool track)
        {
            while (true)
            {
                var result = _repositoryManager.Notification.GetByAccountId(ownerId, track);
                Thread.Sleep(5000);
                return Ok(result);
            }
        }
    }
}
