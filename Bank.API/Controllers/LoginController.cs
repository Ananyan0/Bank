using Bank.Application.DTOs.RegistrationAndLoginDTOs;
using Bank.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ICustomerLoginService _customerLogin;

        public LoginController(ICustomerLoginService customerLogin)
        {
            _customerLogin = customerLogin;
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] CustomerLoginDto loginDto)
        {
            var token = await _customerLogin.LoginAsync(loginDto);

            return Ok(new
            {
                Token = token,
                Message = "You are successfully logged in"
            });
        }
    }
}
