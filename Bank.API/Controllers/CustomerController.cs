using Bank.Application.DTOs;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService CustomerService)
    {
        _customerService = CustomerService;
    }

    [HttpPost] 
    public async Task<IActionResult> CreateCustomerAsync([FromForm] CreateCustomerRequest request)
    {
        var customerId = await _customerService.CreateCustomerAsync(
            request.Name,
            request.Email,
            request.Phone
        );

        return Ok(new { CustomerId = customerId });
    }





    [HttpGet]
    public async Task<ActionResult<List<Customer>>> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("with-accounts")]
    public async Task<ActionResult<List<Customer>>> GetCustomersWithAccounts()
    {
        var customers = await _customerService.GetCustomersWithAccountsAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        try
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}