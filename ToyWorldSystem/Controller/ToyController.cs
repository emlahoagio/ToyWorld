using Contracts;
using Contracts.Services;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/toys")]
    [ApiController]
    public class ToyController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;
        private readonly ICrawlDataJapanFigureServices _crawlDataJapanFigure;
        private readonly IConfiguration _configuration;

        public ToyController(IRepositoryManager repository, IUserAccessor userAccessor, 
            ICrawlDataJapanFigureServices crawlDataJapanFigure, IConfiguration configuration)
        {
            _repository = repository;
            _userAccessor = userAccessor;
            _crawlDataJapanFigure = crawlDataJapanFigure;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetToys([FromQuery] ToyParameters toyParameters)
        {
            var toys = await _repository.Toy.GetAllToys(toyParameters, trackChanges: false);

            if (toys == null || toys.Count() == 0) return NotFound(new { error = "No more items in this page" });
            var pagingNation = new Pagination<ToyInList>
            {
                Count = toys.Count(),
                Data = toys,
                PageNumber = toyParameters.PageNumber,
                PageSize = toyParameters.PageSize
            };

            return Ok(pagingNation);
        }

        [HttpGet]
        [Route("type/{type_name}")]
        public async Task<IActionResult> GetToysByType(string type_name,[FromQuery] ToyParameters toyParameters)
        {
            var toys = await _repository.Toy.GetToysByType(toyParameters ,type_name, trackChanges: false);

            if (toys == null) return NotFound(new { error = "No more result in this page" });

            var pagingNation = new Pagination<ToyInList>
            {
                Count = toys.Count(),
                Data = toys,
                PageNumber = toyParameters.PageNumber,
                PageSize = toyParameters.PageSize
            };

            return Ok(pagingNation);
        }

        [HttpPost]
        [Route("crawl/japanfigure")]
        public async Task<IActionResult> CrawlData([FromHeader]string link_crawl, string toy_type)
        {
            //get list scale figure
            var toyList = await _crawlDataJapanFigure.getToy(link_crawl);

            var type = await _repository.Type.GetTypeByName(toy_type, trackChanges: false);
            if(type == null)
            {
                _repository.Type.CreateType(new Entities.Models.Type { Name = toy_type});
                await _repository.SaveAsync();
                type = await _repository.Type.GetTypeByName(toy_type, trackChanges: false);
            }

            foreach(var toy in toyList)
            {
                toy.TypeId = type.Id;
                var idExistToy = _repository.Toy.IdExistToy(toy.Name);
                if (idExistToy != -1)
                {
                    toy.Id = idExistToy;
                    _repository.Toy.UpdateToy(toy);
                }
                else _repository.Toy.CreateToy(toy);
            }
            await _repository.SaveAsync();

            return Ok();
        }
    }
}
