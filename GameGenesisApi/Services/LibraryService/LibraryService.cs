using AutoMapper;
using GameGenesisApi.Data;
using GameGenesisApi.Dtos.Basket;
using GameGenesisApi.Dtos.Library;
using GameGenesisApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GameGenesisApi.Services.LibraryService
{
    public class LibraryService : ILibraryService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public LibraryService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceLibraryResponse<List<GetLibraryDto>>> AddLibrary(AddLibraryDto newLibrary)
        {
            var serviceResponse = new ServiceLibraryResponse<List<GetLibraryDto>>();
            Library library = _mapper.Map<Library>(newLibrary);
            library.Account = await _context.Accounts.FirstOrDefaultAsync(p => p.Id == newLibrary.AccountId);

            _context.Librarys.Add(library);
            await _context.SaveChangesAsync();

            serviceResponse.Libraries =
                await _context.Librarys.Where(m => m.Account.User.Id == GetUserId()).Select(m => _mapper.Map<GetLibraryDto>(m)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceLibraryResponse<List<GetLibraryDto>>> DeleteLibrary(int id)
        {
            var serviceResponse = new ServiceLibraryResponse<List<GetLibraryDto>>();

            try
            {
                var match = await _context.Librarys.FirstOrDefaultAsync(m => m.Id == id && m.Account.User.Id == GetUserId());
                if (match is null)
                {
                    throw new Exception($"Library with Id '{id}' not found.");
                }

                _context.Librarys.Remove(match);

                await _context.SaveChangesAsync();

                serviceResponse.Libraries = await _context.Librarys
                    .Where(m => m.Account.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetLibraryDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceLibraryResponse<List<GetLibraryDto>>> GetAllLibraries()
        {
            var serviceResponse = new ServiceLibraryResponse<List<GetLibraryDto>>();
            var dbMatchs = await _context.Librarys
                .Include(m => m.Products)
                .Where(m => m.Account.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Libraries = dbMatchs.Select(m => _mapper.Map<GetLibraryDto>(m)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceLibraryResponse<GetLibraryDto>> GetLibraryById(int id)
        {
            var serviceResponse = new ServiceLibraryResponse<GetLibraryDto>();
            var dbLibraries = await _context.Librarys
                .Include(m => m.Products)
                .FirstOrDefaultAsync(m => m.Id == id && m.Account.User.Id == GetUserId());
            serviceResponse.Libraries = _mapper.Map<GetLibraryDto>(dbLibraries);

            return serviceResponse;
        }

        public async Task<ServiceLibraryResponse<GetLibraryDto>> UpdateLibrary(UpdateLibraryDto updatedLibrary)
        {
            var serviceResponse = new ServiceLibraryResponse<GetLibraryDto>();

            try
            {
                var library = await _context.Librarys
                    .FirstOrDefaultAsync(c => c.Id == updatedLibrary.Id);
                if (library is null)
                {
                    throw new Exception($"Match with Id '{updatedLibrary.Id}' not found.");
                }
                if (library.Account.User.Id == GetUserId())
                {
                    //match.Date = updatedLibrary.Date;
                    //match.Opponent = updatedLibrary.Opponent;

                    await _context.SaveChangesAsync();

                    serviceResponse.Libraries = _mapper.Map<GetLibraryDto>(library);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Match with Id '{updatedLibrary.Id}' not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceLibraryResponse<GetLibraryDto>> AddLibraryProduct(AddLibraryProductDto newLibraryProduct)
        {
            var response = new ServiceLibraryResponse<GetLibraryDto>();
            try
            {
                var library = await _context.Librarys
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == newLibraryProduct.LibrariesId);

                if (library is null)
                {
                    response.Success = false;
                    response.Message = "Library not found!";

                    return response;
                }

                var product = await _context.Products.FirstOrDefaultAsync(s => s.Id == newLibraryProduct.ProductsId);
                if (product is null)
                {
                    response.Success = false;
                    response.Message = "Product not found!";

                    return response;
                }

                library.Products!.Add(product);
                await _context.SaveChangesAsync();
                response.Libraries = _mapper.Map<GetLibraryDto>(library);

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
