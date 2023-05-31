using AutoMapper;
using GameGenesisApi.Data;
using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GameGenesisApi.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public CategoryService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceCategoriesResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            var serviceResponse = new ServiceCategoriesResponse<List<GetCategoryDto>>();
            ProductCategory category = _mapper.Map<ProductCategory>(newCategory);
            //category.Player = await _context.Players.FirstOrDefaultAsync(p => p.Id == newCategory.PlayerId);

            _context.ProductCategorys.Add(category);
            await _context.SaveChangesAsync();

            serviceResponse.Categories =
                await _context.ProductCategorys.Select(m => _mapper.Map<GetCategoryDto>(m)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceCategoriesResponse<List<GetCategoryDto>>> DeleteCategory(int id)
        {
            var serviceResponse = new ServiceCategoriesResponse<List<GetCategoryDto>>();

            try
            {
                var match = await _context.ProductCategorys.FirstOrDefaultAsync(m => m.Id == id);
                if (match is null)
                {
                    throw new Exception($"Category with Id '{id}' not found.");
                }

                _context.ProductCategorys.Remove(match);

                await _context.SaveChangesAsync();

                serviceResponse.Categories = await _context.ProductCategorys
                    //.Where(m => m.Player.Team.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCategoryDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceCategoriesResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            var serviceResponse = new ServiceCategoriesResponse<List<GetCategoryDto>>();
            var dbMatchs = await _context.ProductCategorys.ToListAsync();
                //.Include(m => m.Player)
                //.Where(m => m.Player.Team.User.Id == GetUserId() && m.Player.Id == playerId).ToListAsync();
            serviceResponse.Categories = dbMatchs.Select(m => _mapper.Map<GetCategoryDto>(m)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceCategoriesResponse<GetCategoryDto>> GetCategoryById(int id)
        {
            var serviceResponse = new ServiceCategoriesResponse<GetCategoryDto>();
            var dbCategory = await _context.ProductCategorys
                //.Include(m => m.Stat)
                .FirstOrDefaultAsync(m => m.Id == id);
            serviceResponse.Categories = _mapper.Map<GetCategoryDto>(dbCategory);

            return serviceResponse;
        }

        public async Task<ServiceCategoriesResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            var serviceResponse = new ServiceCategoriesResponse<GetCategoryDto>();

            try
            {
                var category = await _context.ProductCategorys
                    .FirstOrDefaultAsync(c => c.Id == updatedCategory.Id);
                if (category is null)
                {
                    throw new Exception($"Category with Id '{updatedCategory.Id}' not found.");
                }
                if (category != null)
                {
                    //match.Date = updatedCategory.Date;
                    //match.Opponent = updatedCategory.Opponent;

                    await _context.SaveChangesAsync();

                    serviceResponse.Categories = _mapper.Map<GetCategoryDto>(category);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Category with Id '{updatedCategory.Id}' not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }
    }
}
