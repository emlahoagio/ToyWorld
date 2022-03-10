﻿using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
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
            return Ok("Success");
        }

        [AllowAnonymous]
        [Route("changestatus")]
        [HttpPut]
        public async Task<IActionResult> Update(int id)
        {
            _repositoryManager.Chat.ChangeStatusChat(id);
            await _repositoryManager.SaveAsync();
            return Ok("Success");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetConversation(int senderId, int receiverId, bool track)
        {
            var result = _repositoryManager.Chat.GetConversation(senderId, receiverId, track);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(int senderId, int receiverId, bool track)
        {
            while (true)
            {
                var result = _repositoryManager.Chat.GetChatByReceiver(receiverId, track);
                Thread.Sleep(5000);
                return Ok(result);
            }
        }
    }
}