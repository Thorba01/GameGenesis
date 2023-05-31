using AutoMapper;
using GameGenesisApi.Data;
using GameGenesisApi.Dtos.Account;
using GameGenesisApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GameGenesisApi.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public AccountService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceAccountResponse<List<GetAccountDto>>> AddAccount(AddAccountDto newAccount)
        {
            var serviceResponse = new ServiceAccountResponse<List<GetAccountDto>>();
            Account account = _mapper.Map<Account>(newAccount);
            account.User = await _context.Users.FirstOrDefaultAsync(p => p.Id == newAccount.UserId);

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            serviceResponse.Accounts =
                await _context.Accounts.Where(m => m.User.Id == GetUserId()).Select(m => _mapper.Map<GetAccountDto>(m)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceAccountResponse<List<GetAccountDto>>> DeleteAccount(int id)
        {
            var serviceResponse = new ServiceAccountResponse<List<GetAccountDto>>();

            try
            {
                var match = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id && m.User.Id == GetUserId());
                if (match is null)
                {
                    throw new Exception($"Match with Id '{id}' not found.");
                }

                _context.Accounts.Remove(match);

                await _context.SaveChangesAsync();

                serviceResponse.Accounts = await _context.Accounts
                    .Where(m => m.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetAccountDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }

        public async Task<ServiceAccountResponse<GetAccountDto>> GetAccountById(int id)
        {
            var serviceResponse = new ServiceAccountResponse<GetAccountDto>();
            var dbAccounts = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id && m.User.Id == GetUserId());
            serviceResponse.Accounts = _mapper.Map<GetAccountDto>(dbAccounts);

            return serviceResponse;
        }

        public async Task<ServiceAccountResponse<List<GetAccountDto>>> GetAllAccounts()
        {
            var serviceResponse = new ServiceAccountResponse<List<GetAccountDto>>();
            var dbAccounts = await _context.Accounts
                .Where(m => m.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Accounts = dbAccounts.Select(m => _mapper.Map<GetAccountDto>(m)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceAccountResponse<GetAccountDto>> UpdateAccount(UpdateAccountDto updatedAccount)
        {
            var serviceResponse = new ServiceAccountResponse<GetAccountDto>();

            try
            {
                var account = await _context.Accounts
                    .Include(m => m.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedAccount.Id);
                if (account is null)
                {
                    throw new Exception($"Account with Id '{updatedAccount.Id}' not found.");
                }
                if (account.User.Id == GetUserId())
                {
                    account.IsActive = updatedAccount.IsActive;

                    await _context.SaveChangesAsync();

                    serviceResponse.Accounts = _mapper.Map<GetAccountDto>(account);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Account with Id '{updatedAccount.Id}' not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }

            return serviceResponse;
        }
    }
}
