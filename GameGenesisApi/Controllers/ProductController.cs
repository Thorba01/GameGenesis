using GameGenesisApi.Dtos.Library;
using GameGenesisApi.Dtos.Product;
using GameGenesisApi.Models;
using GameGenesisApi.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGenesisApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceProductResponse<List<GetProductDto>>>> GetProdcuts()
        {
            return Ok(await _productService.GetAlProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceProductResponse<GetProductDto>>> GetSingleProdcut(int id)
        {
            return Ok(await _productService.GeProductById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceProductResponse<List<GetProductDto>>>> AddProdcut(AddProductDto newProduct)
        {
            return Ok(await _productService.AddProduct(newProduct));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceProductResponse<List<GetProductDto>>>> UpdateProdcut(UpdateProductDto updatedProduct)
        {
            var response = await _productService.UpdateProduct(updatedProduct);
            if (response.Products is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceProductResponse<GetProductDto>>> DeleteProdcut(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.Products is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("Category")]
        public async Task<ActionResult<ServiceProductResponse<GetProductDto>>> AddProductCategory(AddProductCategoryDto newProductCategory)
        {
            return Ok(await _productService.AddProductCategory(newProductCategory));
        }

        [HttpGet("Library")]
        public async Task<ActionResult<ServiceProductResponse<GetProductDto>>> GetAllProductFromLibrary()
        {
            return Ok(await _productService.GetAllProductFromLibrary());
        }

        [HttpGet("Basket")]
        public async Task<ActionResult<ServiceProductResponse<GetProductDto>>> GetAllProductFromBasket()
        {
            return Ok(await _productService.GetAllProductFromBasket());
        }

        [HttpGet("Shop")]
        public async Task<ActionResult<ServiceProductResponse<GetProductDto>>> GetAllProductFromShop()
        {
            return Ok(await _productService.GetAllProductFromShop());
        }
    }
}
