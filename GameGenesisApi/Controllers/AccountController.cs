using GameGenesisApi.Dtos.Account;
using GameGenesisApi.Models;
using GameGenesisApi.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGenesisApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService statService)
        {
            _accountService = statService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceAccountResponse<List<GetAccountDto>>>> GetAccounts()
        {
            return Ok(await _accountService.GetAllAccounts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceAccountResponse<GetAccountDto>>> GetSingleAccount(int id)
        {
            return Ok(await _accountService.GetAccountById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceAccountResponse<List<GetAccountDto>>>> AddAccount(AddAccountDto newAccount)
        {
            return Ok(await _accountService.AddAccount(newAccount));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceAccountResponse<List<GetAccountDto>>>> UpdateAccount(UpdateAccountDto updatedAccount)
        {
            var response = await _accountService.UpdateAccount(updatedAccount);
            if (response.Accounts is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceAccountResponse<GetAccountDto>>> DeleteAccount(int id)
        {
            var response = await _accountService.DeleteAccount(id);
            if (response.Accounts is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
