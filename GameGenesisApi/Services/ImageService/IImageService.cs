using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Dtos.Image;
using GameGenesisApi.Models;

namespace GameGenesisApi.Services.ImageService
{
    public interface IImageService
    {
        Task<ServiceImageResponse<List<GetImageDto>>> GetAllImages();
        Task<ServiceImageResponse<List<GetImageDto>>> DeleteImages(int id);
        Task<ServiceImageResponse<List<GetImageDto>>> AddImage(AddImageDto newImage);

    }
}
