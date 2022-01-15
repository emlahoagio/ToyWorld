using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/types")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public TypeController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        [Route("type_to_conbobox")]
        public async Task<IActionResult> getTypeToCombobox()
        {
            var result = await _repositoryManager.Type.GetListName(trackChanges: false);

            return Ok(result);
        }
    }
}
