using AutoMapper;
using GameGenesisApi.Data;
using GameGenesisApi.Dtos.Product;
using GameGenesisApi.Dtos.Shop;
using GameGenesisApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GameGenesisApi.Services.ShopService
{
    public class ShopService : IShopService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public ShopService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceShopResponse<List<GetShopDto>>> AddShop(AddShopDto newShop)
        {
            var serviceResponse = new ServiceShopResponse<List<GetShopDto>>();
            Shop shop = _mapper.Map<Shop>(newShop);
            //shop.Player = await _context.Players.FirstOrDefaultAsync(p => p.Id == newShop.PlayerId);

            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();

            serviceResponse.Shops =
                await _context.Shops.Select(m => _mapper.Map<GetShopDto>(m)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceShopResponse<List<GetShopDto>>> DeleteShop(int id)
        {
            var serviceResponse = new ServiceShopResponse<List<GetShopDto>>();

            try
            {
                var shop = await _context.Shops.FirstOrDefaultAsync(m => m.Id == id);
                if (shop is null)
                {
                    throw new Exception($"Shop with Id '{id}' not found.");
                }

                _context.Shops.Remove(shop);

                await _context.SaveChangesAsync();

                serviceResponse.Shops = await _context.Shops
                    //.Where(m => m.Player.Team.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetShopDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceShopResponse<List<GetShopDto>>> GetAllShops()
        {
            var serviceResponse = new ServiceShopResponse<List<GetShopDto>>();
            var dbShops = await _context.Shops.ToListAsync();
                //.Include(m => m.Player)
                //.Where(m => m.Player.Team.User.Id == GetUserId() && m.Player.Id == shopId).ToListAsync();
            serviceResponse.Shops = dbShops.Select(m => _mapper.Map<GetShopDto>(m)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceShopResponse<GetShopDto>> GetShopById(int id)
        {
            var serviceResponse = new ServiceShopResponse<GetShopDto>();
            var dbMatchs = await _context.Shops
                //.Include(m => m.Player)
                //.Include(m => m.Stat)
                .FirstOrDefaultAsync();
            serviceResponse.Shops = _mapper.Map<GetShopDto>(dbMatchs);

            return serviceResponse;
        }

        public async Task<ServiceShopResponse<GetShopDto>> UpdateShop(UpdateShopDto updatedShop)
        {
            var serviceResponse = new ServiceShopResponse<GetShopDto>();

            try
            {
                var match = await _context.Shops
                    //.Include(m => m.Player.Team.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedShop.Id);
                if (match is null)
                {
                    throw new Exception($"Match with Id '{updatedShop.Id}' not found.");
                }
                if (match != null)
                {
                    //match.Date = updatedShop.Date;
                    //match.Opponent = updatedShop.Opponent;

                    await _context.SaveChangesAsync();

                    serviceResponse.Shops = _mapper.Map<GetShopDto>(match);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Shop with Id '{updatedShop.Id}' not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceShopResponse<GetShopDto>> AddShopProduct(AddShopproductDto newShopProduct)
        {
            var response = new ServiceShopResponse<GetShopDto>();
            try
            {
                var shop = await _context.Shops
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == newShopProduct.ShopsId);

                if (shop is null)
                {
                    response.Success = false;
                    response.Message = "Shop not found!";

                    return response;
                }

                var product = await _context.Products.FirstOrDefaultAsync(s => s.Id == newShopProduct.ProductsId);
                if (product is null)
                {
                    response.Success = false;
                    response.Message = "Product not found!";

                    return response;
                }

                shop.Products!.Add(product);
                await _context.SaveChangesAsync();
                response.Shops = _mapper.Map<GetShopDto>(shop);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
