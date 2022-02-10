using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/proposals")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public ProposalController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateProposal(NewProposalParameters parameters)
        //{
        //    var accountId = _userAccessor.getAccountId();

        //    var brand = _repository.Brand.GetBrandByName(parameters.BrandName, trackChanges: false);

        //    var type = _repository.Type.GetTypeByName(parameters.TypeName, trackChanges: false) ;

        //    var proposal = new Proposal
        //    {
        //        BrandId = brand.Id,
        //        ContestDescription = parameters.Description,
        //        AccountId = accountId,
        //        MaxRegister = parameters.MaxRegister,
        //        MinRegister = parameters.MinRegister,
                
        //    }
        //}
    }
}
