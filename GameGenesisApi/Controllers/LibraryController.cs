using GameGenesisApi.Dtos.Basket;
using GameGenesisApi.Dtos.Library;
using GameGenesisApi.Models;
using GameGenesisApi.Services.LibraryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGenesisApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService labraryService)
        {
            _libraryService = labraryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceLibraryResponse<List<GetLibraryDto>>>> GetLibraries()
        {
            return Ok(await _libraryService.GetAllLibraries());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceLibraryResponse<GetLibraryDto>>> GetSingleLibrary(int id)
        {
            return Ok(await _libraryService.GetLibraryById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceLibraryResponse<List<GetLibraryDto>>>> AddLibrary(AddLibraryDto newLibrary)
        {
            return Ok(await _libraryService.AddLibrary(newLibrary));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceLibraryResponse<List<GetLibraryDto>>>> UpdateLibrary(UpdateLibraryDto updatedLibrary)
        {
            var response = await _libraryService.UpdateLibrary(updatedLibrary);
            if (response.Libraries is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceLibraryResponse<GetLibraryDto>>> DeleteLibrary(int id)
        {
            var response = await _libraryService.DeleteLibrary(id);
            if (response.Libraries is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("Product")]
        public async Task<ActionResult<ServiceLibraryResponse<GetLibraryDto>>> AddLibraryProduct(AddLibraryProductDto newLibraryProduct)
        {
            return Ok(await _libraryService.AddLibraryProduct(newLibraryProduct));
        }
    }
}
