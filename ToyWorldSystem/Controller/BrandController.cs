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
    [Route("api/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public BrandController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("to_add_favorite")]
        public async Task<IActionResult> GetBrands(PagingParameters paging)
        {
            var brands = await _repository.Brand.GetBrandToAddFavorite(paging, trackChanges: false);

            return Ok(brands);
        }
    }
}
