using GameGenesisApi.Dtos.Basket;
using GameGenesisApi.Dtos.Category;
using GameGenesisApi.Dtos.Library;
using GameGenesisApi.Models;

namespace GameGenesisApi.Services.LibraryService
{
    public interface ILibraryService
    {
        Task<ServiceLibraryResponse<List<GetLibraryDto>>> GetAllLibraries();
        Task<ServiceLibraryResponse<GetLibraryDto>> GetLibraryById(int id);
        Task<ServiceLibraryResponse<List<GetLibraryDto>>> AddLibrary(AddLibraryDto newLibrary);
        Task<ServiceLibraryResponse<GetLibraryDto>> UpdateLibrary(UpdateLibraryDto updatedLibrary);
        Task<ServiceLibraryResponse<List<GetLibraryDto>>> DeleteLibrary(int id);
        Task<ServiceLibraryResponse<GetLibraryDto>> AddLibraryProduct(AddLibraryProductDto newLibraryProduct);

    }
}
