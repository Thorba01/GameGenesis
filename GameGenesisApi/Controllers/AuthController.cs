using GameGenesisApi.Data;
using GameGenesisApi.Dtos.User;
using GameGenesisApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameGenesisApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(AddUserDto request)
        {
            var response = await _authRepo.Register(
                new User { Email = request.Email, Birthdate = request.Birthdate, EmailVerified = false }, request.Password
                );
            Debug.WriteLine(response.Data + "/////////////////////////////////////////////////////////");
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(LoginUserDto request)
        {
            var response = await _authRepo.Login(request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
    }
}
