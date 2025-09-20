using Bank.Application.DTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
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

    
    //Create a new customer
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


    //Get all customers
    [HttpGet]
    public async Task<ActionResult<List<Customer>>> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(customers);
    }

    //Get all customers with accounts
    [HttpGet("with-accounts")]
    public async Task<ActionResult<List<Customer>>> GetCustomersWithAccounts()
    {
        var customers = await _customerService.GetCustomersWithAccountsAsync();
        return Ok(customers);
    }

    //Get customer by id
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }


    //Get customer with profile by id
    [HttpGet("{id}/with-profile")]
    public async Task<ActionResult<CustomerWithProfileResponse>> GetCustomerWithProfile(int id)
    {
        var customer = await _customerService.GetCustomerWithProfileAsync(id);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }


    //Delete customer by id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        await _customerService.DeleteCustomerAsync(id);
        return Ok("success");
    }

    // Delete all customers
    [HttpDelete("Delete all customers")]
    public async Task<IActionResult> DeleteAllCustomers()
    {
        await _customerService.DeleteAllCustomersAsync();
        return Ok("success");
    }
    

    // Update customer by id
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromForm] CustomerUpdateDTO dto)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null) return NotFound();

        customer.Name = dto.Name;
        customer.Email = dto.Email;
        customer.PhoneNumber = dto.Phone;

        await _customerService.UpdateAsync(customer);

        return Ok("success");
    }


}