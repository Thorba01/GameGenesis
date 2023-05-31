using GameGenesisApi.Dtos.Product;
using GameGenesisApi.Dtos.Shop;
using GameGenesisApi.Models;

namespace GameGenesisApi.Services.ShopService
{
    public interface IShopService
    {
        Task<ServiceShopResponse<List<GetShopDto>>> GetAllShops();
        Task<ServiceShopResponse<GetShopDto>> GetShopById(int id);
        Task<ServiceShopResponse<List<GetShopDto>>> AddShop(AddShopDto newShop);
        Task<ServiceShopResponse<GetShopDto>> UpdateShop(UpdateShopDto updatedShop);
        Task<ServiceShopResponse<List<GetShopDto>>> DeleteShop(int id);
        Task<ServiceShopResponse<GetShopDto>> AddShopProduct(AddShopproductDto newShopProduct);
    }
}
