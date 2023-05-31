using GameGenesisApi.Dtos.Account;
using GameGenesisApi.Dtos.Basket;
using GameGenesisApi.Models;

namespace GameGenesisApi.Services.BasketService
{
    public interface IBasketService
    {
        Task<ServiceBasketResponse<List<GetBasketDto>>> GetAllBaskets();
        Task<ServiceBasketResponse<GetBasketDto>> GetBasketById(int id);
        Task<ServiceBasketResponse<List<GetBasketDto>>> AddBasket(AddbasketDto newBasket);
        Task<ServiceBasketResponse<GetBasketDto>> UpdateBasket(UpdateBasketDto updatedBasket);
        Task<ServiceBasketResponse<List<GetBasketDto>>> DeleteBasket(int id);
        Task<ServiceBasketResponse<GetBasketDto>> AddBasketProduct(AddBasketProductDto newBasketProduct);
        Task<ServiceBasketResponse<GetBasketDto>> RemoveBasketProduct(AddBasketProductDto removeBasketProduct);
    }
}
