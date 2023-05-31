using GameGenesisApi.Dtos.Shop;
using GameGenesisApi.Models;
using GameGenesisApi.Services.ShopService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGenesisApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceShopResponse<List<GetShopDto>>>> GetShops()
        {
            return Ok(await _shopService.GetAllShops());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceShopResponse<GetShopDto>>> GetSingleShop(int id)
        {
            return Ok(await _shopService.GetShopById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceShopResponse<List<GetShopDto>>>> AddShop(AddShopDto newShop)
        {
            return Ok(await _shopService.AddShop(newShop));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceShopResponse<List<GetShopDto>>>> UpdateShop(UpdateShopDto updatedShop)
        {
            var response = await _shopService.UpdateShop(updatedShop);
            if (response.Shops is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceShopResponse<GetShopDto>>> DeleteShop(int id)
        {
            var response = await _shopService.DeleteShop(id);
            if (response.Shops is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("Product")]
        public async Task<ActionResult<ServiceShopResponse<GetShopDto>>> AddShopProduct(AddShopproductDto newShopProduct)
        {
            return Ok(await _shopService.AddShopProduct(newShopProduct));
        }
    }
}
