using Bank.Application.DTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/customer-profiles")]
public class CustomerProfileController : ControllerBase
{
    private readonly ICustomerProfileService _service;

    public CustomerProfileController(ICustomerProfileService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<CustomerProfile>> Create(CreateCustomerProfileRequest dto)
    {
        var profile = await _service.CreateProfileAsync(dto.CustomerId, dto.Address, dto.PassportNumber, dto.DateOfBirth);
        if (profile == null)
            return NotFound($"Customer with Id {dto.CustomerId} not found.");

        return Ok(profile);
    }
}
