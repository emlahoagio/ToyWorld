using Contracts;
using Entities.RequestFeatures;
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
        private readonly IRepositoryManager _repository;

        public TypeController(IRepositoryManager repositoryManager)
        {
            _repository = repositoryManager;
        }

        /// <summary>
        /// get type name for combobox select type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("type_to_conbobox")]
        public async Task<IActionResult> getTypeToCombobox()
        {
            var result = await _repository.Type.GetListName(trackChanges: false);

            return Ok(result);
        }

        [HttpGet]
        [Route("to_add_favorite")]
        public async Task<IActionResult> GetBrands(PagingParameters paging)
        {
            var brands = await _repository.Type.GetTypeToAddFavorite(paging, trackChanges: false);

            return Ok(brands);
        }
    }
}
