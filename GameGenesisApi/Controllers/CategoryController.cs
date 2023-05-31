using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Models;
using GameGenesisApi.Services.BasketService;
using GameGenesisApi.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGenesisApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceCategoriesResponse<List<GetCategoryDto>>>> GetCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceCategoriesResponse<GetCategoryDto>>> GetSingleCategory(int id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceCategoriesResponse<List<GetCategoryDto>>>> AddCategory(AddCategoryDto newMatch)
        {
            return Ok(await _categoryService.AddCategory(newMatch));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceCategoriesResponse<List<GetCategoryDto>>>> UpdateCategory(UpdateCategoryDto updatedMatch)
        {
            var response = await _categoryService.UpdateCategory(updatedMatch);
            if (response.Categories is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceCategoriesResponse<GetCategoryDto>>> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategory(id);
            if (response.Categories is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
