using AutoMapper;
using GameGenesisApi.Data;
using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Dtos.Image;
using GameGenesisApi.Dtos.Product;
using GameGenesisApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameGenesisApi.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public ImageService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceImageResponse<List<GetImageDto>>> AddImage(AddImageDto newImage)
        {
            var serviceResponse = new ServiceImageResponse<List<GetImageDto>>();
            Image image = _mapper.Map<Image>(newImage);
            image.Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == newImage.ProductId);

            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            serviceResponse.Images =
                await _context.Images.Select(m => _mapper.Map<GetImageDto>(m)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceImageResponse<List<GetImageDto>>> DeleteImages(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            var serviceResponse = new ServiceImageResponse<List<GetImageDto>>();

            if (user != null && user.IsAdmin)
            {

                try
                {
                    var image = await _context.Images.FirstOrDefaultAsync(m => m.Id == id);
                    if (image is null)
                    {
                        throw new Exception($"Image with Id '{id}' not found.");
                    }

                    _context.Images.Remove(image);

                    await _context.SaveChangesAsync();

                    serviceResponse.Images = await _context.Images
                        .Select(c => _mapper.Map<GetImageDto>(c)).ToListAsync();
                }
                catch (Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;

                }
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "You don't have the rights to do this action ! ";
            }

            return serviceResponse;
        }

        public async Task<ServiceImageResponse<List<GetImageDto>>> GetAllImages()
        {
            var serviceResponse = new ServiceImageResponse<List<GetImageDto>>();
            var dbImages = await _context.Images.ToListAsync();

            serviceResponse.Images = dbImages.Select(m => _mapper.Map<GetImageDto>(m)).ToList();

            return serviceResponse;
        }
    }
}
