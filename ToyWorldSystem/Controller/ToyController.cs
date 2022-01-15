using Contracts;
using Contracts.Services;
using Entities.DataTransferObject;
using Entities.ErrorModel;
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

            return Ok(toys);
        }

        [HttpPost]
        [Route("crawl/japanfigure")]
        public async Task<IActionResult> CrawlData(string link_crawl, string toy_type)
        {
            //check valid link
            if (!link_crawl.Contains("https://japanfigure.vn/collections/"))
                throw new ErrorDetails(HttpStatusCode.BadRequest, "The link you select isn't from japanfigure.vn");

            //get list toy from link
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
