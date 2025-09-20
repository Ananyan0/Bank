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
    public async Task<ActionResult<CustomerProfile>> Create([FromForm] CreateCustomerProfileRequest dto)
    {
        var profile = await _service.CreateProfileAsync(dto.CustomerId, dto.Address, dto.PassportNumber, dto.DateOfBirth);
        if (profile == null)
            return NotFound($"Customer with Id {dto.CustomerId} not found.");

        return Ok(profile);
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> Delete(int customerId)
    {
        try
        {
            await _service.DeleteProfileAsync(customerId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Customer with Id {customerId} not found.");
        }
    }
}