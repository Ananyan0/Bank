using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.Interfaces;
using Bank.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _service;

        public DirectorController(IDirectorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirector([FromForm] CreateDirectorRequest request)
        {
            var response = await _service.CreateDirectorAsync(request);
            return Ok(response);
        }


        [HttpGet("Get all directors, with their branches")]
        public async Task<ActionResult<List<Director>>> GetAllDirectorsWithBranches()
        {

            var directors = await _service.GetDirectorsWithBracnhesAsync();
            
            return Ok(directors);
        }
    }
}
