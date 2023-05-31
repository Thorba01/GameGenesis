using AutoMapper;
using GameGenesisApi.Data;
using GameGenesisApi.Dtos.Basket;
using GameGenesisApi.Dtos.Shop;
using GameGenesisApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GameGenesisApi.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public BasketService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceBasketResponse<List<GetBasketDto>>> AddBasket(AddbasketDto newBasket)
        {
            var serviceResponse = new ServiceBasketResponse<List<GetBasketDto>>();
            Basket basket = _mapper.Map<Basket>(newBasket);
            basket.Account = await _context.Accounts.FirstOrDefaultAsync(p => p.Id == newBasket.AccountId);

            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();

            serviceResponse.Baskets =
                await _context.Accounts.Where(m => m.User.Id == GetUserId()).Select(m => _mapper.Map<GetBasketDto>(m)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceBasketResponse<List<GetBasketDto>>> DeleteBasket(int id)
        {
            var serviceResponse = new ServiceBasketResponse<List<GetBasketDto>>();

            try
            {
                var basket = await _context.Baskets.FirstOrDefaultAsync(m => m.Id == id && m.Account.User.Id == GetUserId());
                if (basket is null)
                {
                    throw new Exception($"Basket with Id '{id}' not found.");
                }

                _context.Baskets.Remove(basket);

                await _context.SaveChangesAsync();

                serviceResponse.Baskets = await _context.Baskets
                    .Where(m => m.Account.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetBasketDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceBasketResponse<List<GetBasketDto>>> GetAllBaskets()
        {
            var serviceResponse = new ServiceBasketResponse<List<GetBasketDto>>();
            var dbBaskets = await _context.Baskets
                //.Include(m => m.Player)
                .Where(m => m.Account.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Baskets = dbBaskets.Select(m => _mapper.Map<GetBasketDto>(m)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceBasketResponse<GetBasketDto>> GetBasketById(int id)
        {
            var serviceResponse = new ServiceBasketResponse<GetBasketDto>();
            var dbBaskets = await _context.Baskets
                //.Include(m => m.Stat)
                .FirstOrDefaultAsync(m => m.Id == id && m.Account.User.Id == GetUserId());
            serviceResponse.Baskets = _mapper.Map<GetBasketDto>(dbBaskets);

            return serviceResponse;
        }

        public async Task<ServiceBasketResponse<GetBasketDto>> UpdateBasket(UpdateBasketDto updatedBasket)
        {
            var serviceResponse = new ServiceBasketResponse<GetBasketDto>();

            try
            {
                var match = await _context.Baskets
                    .FirstOrDefaultAsync(c => c.Id == updatedBasket.Id);
                if (match is null)
                {
                    throw new Exception($"Match with Id '{updatedBasket.Id}' not found.");
                }
                if (match.Account.User.Id == GetUserId())
                {
                    //match.Date = updatedBasket.Date;
                    //match.Opponent = updatedBasket.Opponent;

                    await _context.SaveChangesAsync();

                    serviceResponse.Baskets = _mapper.Map<GetBasketDto>(match);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Match with Id '{updatedBasket.Id}' not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceBasketResponse<GetBasketDto>> AddBasketProduct(AddBasketProductDto newBasketProduct)
        {
            var response = new ServiceBasketResponse<GetBasketDto>();
            try
            {
                var basket = await _context.Baskets
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == newBasketProduct.BasketsId);

                if (basket is null)
                {
                    response.Success = false;
                    response.Message = "Basket not found!";

                    return response;
                }

                var product = await _context.Products.FirstOrDefaultAsync(s => s.Id == newBasketProduct.ProductsId);
                if (product is null)
                {
                    response.Success = false;
                    response.Message = "Product not found!";

                    return response;
                }

                basket.Products!.Add(product);
                await _context.SaveChangesAsync();
                response.Baskets = _mapper.Map<GetBasketDto>(basket);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceBasketResponse<GetBasketDto>> RemoveBasketProduct(AddBasketProductDto removeBasketProduct)
        {
            var response = new ServiceBasketResponse<GetBasketDto>();
            try
            {
                var basket = await _context.Baskets
                    .Include(b => b.Products)
                    .FirstOrDefaultAsync(b => b.Id == removeBasketProduct.BasketsId);

                if (basket is null)
                {
                    response.Success = false;
                    response.Message = "Basket not found!";

                    return response;
                }

                var product = await _context.Products.FirstOrDefaultAsync(s => s.Id == removeBasketProduct.ProductsId);
                if (product is null)
                {
                    response.Success = false;
                    response.Message = "Product not found!";

                    return response;
                }

                basket.Products!.Remove(product);
                await _context.SaveChangesAsync();
                response.Baskets = _mapper.Map<GetBasketDto>(basket);

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
