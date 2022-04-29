using Contracts;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public ImagesController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        #region Get image by id
        /// <summary>
        /// Get Image (Role: ALL)
        /// </summary>
        /// <param name="image_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{image_id}")]
        public async Task<IActionResult> GetImage(int image_id)
        {
            var image = await _repository.Image.GetImageById(image_id, trackChanges: false);

            if (image == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "Invalid image Id");

            return Ok(image);
        }
        #endregion

        #region Get image of prize
        /// <summary>
        /// Get Image for prize in contest
        /// </summary>
        /// <param name="prize_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("prize/{prize_id}")]
        public async Task<IActionResult> GetImageForPrize(int prize_id)
        {
            var images = await _repository.Image.GetImageForPrize(prize_id, trackChanges: false);

            return Ok(images);
        }
        #endregion

        #region Create new image
        /// <summary>
        /// Create new image (Role: ALL)
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateImage(Entities.Models.Image image)
        {
            _repository.Image.Create(image);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Delete image
        /// <summary>
        /// Delete image (Role: ALL)
        /// </summary>
        /// <param name="image_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{image_id}")]
        public async Task<IActionResult> DeleteImage(int image_id)
        {
            await _repository.Image.Delete(image_id, trackChanges: false);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion
    }
}
