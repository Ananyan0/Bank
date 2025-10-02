using Bank.Application.DTOs.RegistrationDTO;
using Bank.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _service;

        public RegistrationController(IRegistrationService service)
        {
            _service = service;
        }



        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> Register([FromForm] CustomerRegistrationDto register)
        {
            var tokens = await _service.RegisterCustomerAsync(register);

            return Ok(new
            {
                Token = tokens,
                Message = "You are successfully registered"
            });
        }
    }
}
