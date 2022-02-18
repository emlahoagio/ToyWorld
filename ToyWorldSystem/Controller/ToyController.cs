using Contracts;
using Contracts.Services;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Repository.Services;
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
        private readonly ICrawlDataMyKingdomService _kingdom;

        public ToyController(IRepositoryManager repository, IUserAccessor userAccessor,
            ICrawlDataJapanFigureServices crawlDataJapanFigure, IConfiguration configuration, ICrawlDataMyKingdomService kingdom)
        {
            _repository = repository;
            _userAccessor = userAccessor;
            _crawlDataJapanFigure = crawlDataJapanFigure;
            _configuration = configuration;
            _kingdom = kingdom;
        }

        /// <summary>
        /// Get all toys for the home page (Role: Manager, Members)
        /// </summary>
        /// <param name="toyParameters">2 param: PageNumber is number of page, PageSize is the number of item in page</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetToys([FromQuery] PagingParameters toyParameters)
        {
            var toys = await _repository.Toy.GetAllToys(toyParameters, trackChanges: false);

            return Ok(toys);
        }

        /// <summary>
        /// Get all toy by type name
        /// </summary>
        /// <param name="type_name">Name is get from combobox</param>
        /// <param name="toyParameters">2 param: PageNumber is number of page, PageSize is the number of item in page</param>
        /// <returns></returns>
        [HttpGet]
        [Route("type/{type_name}")]
        public async Task<IActionResult> GetToysByType(string type_name, [FromQuery] PagingParameters toyParameters)
        {
            var toys = await _repository.Toy.GetToysByType(toyParameters, type_name, trackChanges: false);

            return Ok(toys);
        }

        /// <summary>
        /// Get detail of toy by toy Id
        /// </summary>
        /// <param name="toy_id">Id return in get list toy</param>
        /// <returns></returns>
        [HttpGet]
        [Route("details/{toy_id}")]
        public async Task<IActionResult> GetToyDetailById(int toy_id)
        {
            var toyDetail = await _repository.Toy.GetToyDetail(toy_id, trackChanges: false);

            return Ok(toyDetail);
        }

        /// <summary>
        /// Crawl data from Japan Figure (Still not done)
        /// </summary>
        /// <param name="link_crawl">JapanFigure link</param>
        /// <param name="toy_type">Name of type</param>
        /// <returns></returns>
        [HttpPost]
        [Route("crawl/japanfigure")]
        public async Task<IActionResult> CrawlData(string link_crawl, string toy_type)
        {
            //get list scale figure
            var toyList = await _crawlDataJapanFigure.getToy(link_crawl);

            var type = await _repository.Type.GetTypeByName(toy_type, trackChanges: false);
            if (type == null)
            {
                _repository.Type.CreateType(new Entities.Models.Type { Name = toy_type });
                await _repository.SaveAsync();
                type = await _repository.Type.GetTypeByName(toy_type, trackChanges: false);
            }

            foreach (var toy in toyList)
            {
                toy.TypeId = type.Id;
                var existToy = await _repository.Toy.GetExistToy(toy.Name);
                if (existToy != null)
                {
                    toy.Id = existToy.Id;
                    toy.Images = existToy.Images;
                    _repository.Toy.UpdateToy(toy);
                }
                else _repository.Toy.CreateToy(toy);
            }
            await _repository.SaveAsync();

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("crawl/mykingdom")]
        public async Task<IActionResult> KingdomCrawl(string url)
        {
            List<String> listLinkToy = _kingdom.GetListLink(url);
            foreach (var i in listLinkToy)
            {
                Toy dto = await _kingdom.GetToyDetail(i);
                var existToy = await _repository.Toy.GetExistToy(dto.Name);
                if (existToy == null)
                {
                    _repository.Toy.CreateToy(dto);
                }
                else
                {
                    _repository.Toy.UpdateToy(dto);
                }
            }
            await _repository.SaveAsync();
            return Ok();
        }
    }
}
