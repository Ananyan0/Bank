using AutoMapper;
using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Interfaces;
using Bank.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _service;
        private readonly IMapper _mapper;

        public DirectorController(IDirectorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDirector([FromForm] CreateDirectorRequest request)
        {
            var response = await _service.CreateDirectorAsync(request);
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("Get all directors, with their branches")]
        public async Task<ActionResult<List<Director>>> GetAllDirectorsWithBranches()
        {

            var directors = await _service.GetDirectorsWithBracnhesAsync();

            var response = _mapper.Map<List<DirectorResponseDto>>(directors);

            return Ok(response);
        }
    }
}
