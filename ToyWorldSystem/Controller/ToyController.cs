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

        /// <summary>
        /// Get all toys for the home page (Role: Manager, Members)
        /// </summary>
        /// <param name="toyParameters">2 param: PageNumber is number of page, PageSize is the number of item in page</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get all toy by type name
        /// </summary>
        /// <param name="type_name">Name is get from combobox</param>
        /// <param name="toyParameters">2 param: PageNumber is number of page, PageSize is the number of item in page</param>
        /// <returns></returns>
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

        /// <summary>
        /// Crawl data from Japan Figure (Still not done)
        /// </summary>
        /// <param name="link_crawl">JapanFigure link</param>
        /// <param name="toy_type">Name of type</param>
        /// <returns></returns>
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
