using GameGenesisApi.Dtos.Basket;
using GameGenesisApi.Dtos.Shop;
using GameGenesisApi.Models;
using GameGenesisApi.Services.BasketService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGenesisApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class Basketcontroller : ControllerBase
    {
        private readonly IBasketService _basketService;

        public Basketcontroller(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceBasketResponse<List<GetBasketDto>>>> GetBaskets()
        {
            return Ok(await _basketService.GetAllBaskets());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceBasketResponse<GetBasketDto>>> GetSingleBasket(int id)
        {
            return Ok(await _basketService.GetBasketById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceBasketResponse<List<GetBasketDto>>>> AddBasket(AddbasketDto newBasket)
        {
            return Ok(await _basketService.AddBasket(newBasket));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceBasketResponse<List<GetBasketDto>>>> UpdateBasket(UpdateBasketDto updatedBasket)
        {
            var response = await _basketService.UpdateBasket(updatedBasket);
            if (response.Baskets is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceBasketResponse<GetBasketDto>>> DeleteBasket(int id)
        {
            var response = await _basketService.DeleteBasket(id);
            if (response.Baskets is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("Product")]
        public async Task<ActionResult<ServiceBasketResponse<GetBasketDto>>> AddBasketProduct(AddBasketProductDto newBasketProduct)
        {
            return Ok(await _basketService.AddBasketProduct(newBasketProduct));
        }

        [HttpDelete("Product")]
        public async Task<ActionResult<ServiceBasketResponse<GetBasketDto>>> RemoveBasketProduct(AddBasketProductDto removeBasketProduct)
        {
            return Ok(await _basketService.RemoveBasketProduct(removeBasketProduct));
        }
    }
}
