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
    [Route("api/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public BrandController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        #region Get type for home page
        /// <summary>
        /// Get type to add favorite type (All role)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("to_add_favorite")]
        public async Task<IActionResult> GetBrands()
        {
            var account_id = _userAccessor.getAccountId();
            var brands = await _repository.Brand.GetBrandToAddFavorite(account_id, trackChanges: false);

            return Ok(brands);
        }
        #endregion

        #region Add/Remove favorite
        /// <summary>
        /// Add/remove favorite type (All role)
        /// </summary>
        /// <param name="brands_id">If not in favorite -> add, Has in favorite -> remove</param>
        /// <returns></returns>
        [HttpPost]
        [Route("favorite/unfavorite")]
        public async Task<IActionResult> AddFavoriteType(List<int> brands_id)
        {
            var account_id = _userAccessor.getAccountId();

            var favorite_brand = await _repository.FavoriteBrand.GetFavoriteBrand(account_id, trackChanges: false);
            foreach (var brand_id in brands_id)
            {
                if (!_repository.FavoriteBrand.IsFavoriteBrand(favorite_brand, brand_id))
                {
                    _repository.FavoriteBrand.Create(new FavoriteBrand { AccountId = account_id, BrandId = brand_id });
                }
                else
                {
                    _repository.FavoriteBrand.Delete(new FavoriteBrand { AccountId = account_id, BrandId = brand_id });
                }
            }

            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion
    }
}
