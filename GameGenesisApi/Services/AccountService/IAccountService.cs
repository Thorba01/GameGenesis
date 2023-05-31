using GameGenesisApi.Dtos.Account;
using GameGenesisApi.Models;

namespace GameGenesisApi.Services.AccountService
{
    public interface IAccountService
    {
        Task<ServiceAccountResponse<List<GetAccountDto>>> GetAllAccounts();
        Task<ServiceAccountResponse<GetAccountDto>> GetAccountById(int id);
        Task<ServiceAccountResponse<List<GetAccountDto>>> AddAccount(AddAccountDto newMatch);
        Task<ServiceAccountResponse<GetAccountDto>> UpdateAccount(UpdateAccountDto updatedMatch);
        Task<ServiceAccountResponse<List<GetAccountDto>>> DeleteAccount(int id);
    }
}
