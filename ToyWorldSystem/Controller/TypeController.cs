using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/types")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public TypeController(IRepositoryManager repositoryManager, IUserAccessor userAccessor)
        {
            _repository = repositoryManager;
            _userAccessor = userAccessor;
        }

        #region Get all type of toy
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
        #endregion

        #region Get favorite type
        /// <summary>
        /// Get type for add favorite
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("to_add_favorite")]
        public async Task<IActionResult> GetTypeForFavorite()
        {
            var account_id = _userAccessor.GetAccountId();

            var brands = await _repository.Type.GetTypeToAddFavorite(account_id, trackChanges: false);

            return Ok(brands);
        }
        #endregion

        #region Add/ remove favorite type
        /// <summary>
        /// Add/remove favorite type (All role)
        /// </summary>
        /// <param name="types_id">If not in favorite -> add, Has in favorite -> remove</param>
        /// <returns></returns>
        [HttpPost]
        [Route("favorite/unfavorite")]
        public async Task<IActionResult> AddFavoriteType(List<int> types_id)
        {
            var account_id = _userAccessor.GetAccountId();

            var favorite_type = await _repository.FavoriteType.GetFavoriteType(account_id, trackChanges: false);
            foreach(var type_id in types_id)
            {
                if(!_repository.FavoriteType.IsFavoriteType(favorite_type, type_id))
                {
                    _repository.FavoriteType.Create(new FavoriteType { AccountId = account_id, TypeId = type_id });
                }else
                {
                    _repository.FavoriteType.Delete(new FavoriteType { AccountId = account_id, TypeId = type_id });
                }
            }

            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion
    }
}
