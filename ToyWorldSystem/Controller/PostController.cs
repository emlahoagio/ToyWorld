using Contracts;
using Entities.ErrorModel;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public PostController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        [Route("group/{group_id}")]
        public async Task<IActionResult> GetListPostByGroup(int group_id, [FromQuery]PagingParameters paging)
        {
            var result = await _repositoryManager.Post.GetPostByGroupId(group_id, trackChanges: false, paging);

            if(result == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No more posts in this group");
            }

            return Ok(result);
        }
    }
}
