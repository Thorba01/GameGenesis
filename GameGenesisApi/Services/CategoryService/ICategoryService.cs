using GameGenesisApi.Dtos.Basket;
using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Models;

namespace GameGenesisApi.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceCategoriesResponse<List<GetCategoryDto>>> GetAllCategories();
        Task<ServiceCategoriesResponse<GetCategoryDto>> GetCategoryById(int id);
        Task<ServiceCategoriesResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory);
        Task<ServiceCategoriesResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory);
        Task<ServiceCategoriesResponse<List<GetCategoryDto>>> DeleteCategory(int id);
    }
}
