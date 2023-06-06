using AutoMapper;
using Azure;
using GameGenesisApi.Data;
using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Dtos.Image;
using GameGenesisApi.Dtos.Library;
using GameGenesisApi.Dtos.Product;
using GameGenesisApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GameGenesisApi.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public ProductService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceProductResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceResponse = new ServiceProductResponse<List<GetProductDto>>();

            Product product = _mapper.Map<Product>(newProduct);
            product.Author = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            serviceResponse.Products =
                await _context.Products.Select(m => _mapper.Map<GetProductDto>(m)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceProductResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            var serviceResponse = new ServiceProductResponse<List<GetProductDto>>();

            if(user != null && user.IsAdmin)
            {

                try
                {
                    var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
                    if (product is null)
                    {
                        throw new Exception($"Match with Id '{id}' not found.");
                    }

                    _context.Products.Remove(product);

                    await _context.SaveChangesAsync();

                    serviceResponse.Products = await _context.Products
                        //.Where(m => m.Player.Team.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetProductDto>(c)).ToListAsync();
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

        public async Task<ServiceProductResponse<GetProductDto>> GeProductById(int id)
        {
            var serviceResponse = new ServiceProductResponse<GetProductDto>();
            var dbProduct = await _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            serviceResponse.Products = _mapper.Map<GetProductDto>(dbProduct);
            serviceResponse.Products.Categories = dbProduct!.ProductCategories!.Select(m => _mapper.Map<GetCategoryDto>(m)).ToList();
            serviceResponse.Products.Images = dbProduct!.Images!.Select(m => _mapper.Map<GetImageDto>(m)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceProductResponse<List<GetProductDto>>> GetAlProducts()
        {
            var serviceResponse = new ServiceProductResponse<List<GetProductDto>>();
            var dbProducts = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.ProductCategories).ToListAsync();

            serviceResponse.Products = dbProducts.Select(m => _mapper.Map<GetProductDto>(m)).ToList();

            foreach (GetProductDto product in serviceResponse.Products)
            {
                var dbprod = await _context.Products.Include(p => p.ProductCategories).FirstOrDefaultAsync(p => p.Id == product.Id);
                product.Categories = dbprod!.ProductCategories!.Select(m => _mapper.Map<GetCategoryDto> (m)).ToList();
                product.Images = dbprod!.Images!.Select(m => _mapper.Map<GetImageDto> (m)).ToList();
            }        

            return serviceResponse;
        }

        public async Task<ServiceProductResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            var serviceResponse = new ServiceProductResponse<GetProductDto>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            if(user != null && user.IsAdmin)
            {
                try
                {
                    var product = await _context.Products
                        .FirstOrDefaultAsync(c => c.Id == updatedProduct.Id);
                    if (product is null)
                    {
                        throw new Exception($"Product with Id '{updatedProduct.Id}' not found.");
                    }
                    if (product != null)
                    {
                        product.Name = updatedProduct.Name;
                        product.Description = updatedProduct.Description;
                        product.Price = updatedProduct.Price;
                    
                        await _context.SaveChangesAsync();

                        serviceResponse.Products= _mapper.Map<GetProductDto>(product);
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = $"Match with Id '{updatedProduct.Id}' not found.";
                    }
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

        public async Task<ServiceProductResponse<GetProductDto>> AddProductCategory(AddProductCategoryDto newProductCategory)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            var response = new ServiceProductResponse<GetProductDto>();

            if (user != null && user.IsAdmin)
            {
                try
                {
                    var product = await _context.Products
                        .Include(c => c.ProductCategories)
                        .FirstOrDefaultAsync(p => p.Id == newProductCategory.ProductsId);
                    if (product is null)
                    {
                        response.Success = false;
                        response.Message = "Product not found !";

                        return response;
                    }

                    var category = await _context.ProductCategorys.FirstOrDefaultAsync(c => c.Id == newProductCategory.ProductCategoriesId);
                    if (category is null)
                    {
                        response.Success = false;
                        response.Message = "Category not found ! ";

                        return response;
                    }

                    product.ProductCategories!.Add(category);
                    await _context.SaveChangesAsync();
                    response.Products = _mapper.Map<GetProductDto>(product);
                    response.Products.Categories = product!.ProductCategories!.Select(m => _mapper.Map<GetCategoryDto>(m)).ToList();
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "You don't have the rights to do this action ! ";
            }

            return response;
        }

        public async Task<ServiceProductResponse<List<GetProductDto>>> GetAllProductFromLibrary()
        {
            var serviceResponse = new ServiceProductResponse<List<GetProductDto>>();
            var productList = new List<Product>();

            var dbLibrary = await _context.Librarys
            .Include(l => l.Products)
            .Where(l => l.Account.UserId == GetUserId()).ToListAsync();

            foreach(Library l in dbLibrary)
            {
                if(l.Products is not null)
                {
                    foreach(Product p in l.Products)
                    {
                        productList.Add(p);
                    }
                }
            }

            serviceResponse.Products = productList.Select(m => _mapper.Map<GetProductDto>(m)).ToList();
            
            return serviceResponse;
        }

        public async Task<ServiceProductResponse<List<GetProductDto>>> GetAllProductFromBasket()
        {
            var serviceResponse = new ServiceProductResponse<List<GetProductDto>>();
            var productList = new List<Product>();

            var dbProducts = await _context.Baskets
            .Include(b => b.Products)
            .Where(b => b.Account.UserId == GetUserId()).ToListAsync();

            foreach (Basket b in dbProducts)
            {
                if (b.Products is not null)
                {
                    foreach (Product p in b.Products)
                    {
                        productList.Add(p);
                    }
                }
            }

            serviceResponse.Products = productList.Select(m => _mapper.Map<GetProductDto>(m)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceProductResponse<List<GetProductDto>>> GetAllProductFromShop()
        {
            var serviceResponse = new ServiceProductResponse<List<GetProductDto>>();
            var productList = new List<Product>();

            var dbProducts = await _context.Shops
            .Include(s => s.Products)
            .ToListAsync();

            foreach (Shop s in dbProducts)
            {
                if (s.Products is not null)
                {
                    foreach (Product p in s.Products)
                    {
                        productList.Add(p);
                    }
                }
            }

            serviceResponse.Products = productList.Select(m => _mapper.Map<GetProductDto>(m)).ToList();

            return serviceResponse;
        }
    }
}
