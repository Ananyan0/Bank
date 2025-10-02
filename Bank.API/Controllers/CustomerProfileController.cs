using AutoMapper;
using Bank.Application.DTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/customer-profiles")]
public class CustomerProfileController : ControllerBase
{
    private readonly ICustomerProfileService _service;
    private readonly IMapper _mapper;

    public CustomerProfileController(ICustomerProfileService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }


    [Authorize(Roles = "Admin")]
    [HttpPost("Create profile")]
    public async Task<ActionResult<CustomerProfile>> Create([FromForm] CreateCustomerProfileRequest request)
    {
        var profile = await _service.CreateProfileAsync(request);

        return Ok(profile);
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{customerId}")]
    public async Task<IActionResult> Delete(int customerId)
    {
        await _service.DeleteProfileAsync(customerId);
        return NoContent();

    }
}