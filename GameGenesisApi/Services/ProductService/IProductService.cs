using GameGenesisApi.Dtos.Library;
using GameGenesisApi.Dtos.Product;
using GameGenesisApi.Models;

namespace GameGenesisApi.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceProductResponse<List<GetProductDto>>> GetAlProducts();
        Task<ServiceProductResponse<GetProductDto>> GeProductById(int id);
        Task<ServiceProductResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct);
        Task<ServiceProductResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct);
        Task<ServiceProductResponse<List<GetProductDto>>> DeleteProduct(int id);
        Task<ServiceProductResponse<GetProductDto>> AddProductCategory(AddProductCategoryDto newProductCategory);
        Task<ServiceProductResponse<List<GetProductDto>>> GetAllProductFromLibrary();
        Task<ServiceProductResponse<List<GetProductDto>>> GetAllProductFromBasket();
        Task<ServiceProductResponse<List<GetProductDto>>> GetAllProductFromShop();
    }
}
