using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Dtos.Image;
using GameGenesisApi.Models;
using GameGenesisApi.Services.ImageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGenesisApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceImageResponse<List<GetImageDto>>>> GetImages()
        {
            return Ok(await _imageService.GetAllImages());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceImageResponse<List<GetImageDto>>>> AddImage(AddImageDto newImage)
        {
            return Ok(await _imageService.AddImage(newImage));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceImageResponse<GetImageDto>>> DeleteImage(int id)
        {
            var response = await _imageService.DeleteImages(id);
            if (response.Images is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
