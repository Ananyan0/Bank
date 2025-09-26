using AutoMapper;
using Bank.Application.DTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Mapping.ByCode.Impl;

namespace Bank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;


    public CustomerController(ICustomerService CustomerService, IMapper mapper)
    {
        _customerService = CustomerService;
        _mapper = mapper;
    }


    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync([FromForm] CreateCustomerRequest request)
    {
        var customer = _mapper.Map<Customer>(request);
        var customerId = await _customerService.CreateCustomerAsync(customer);

        return Ok(new { CustomerId = customerId });
    }



    [HttpGet]
    public async Task<ActionResult<List<CustomerResponseDTO>>> GetAll()
    {
        var customers = await _customerService.GetAllAsync();

        var customerDtos = _mapper.Map<List<CustomerResponseDTO>>(customers);

        return Ok(customerDtos);
    }


    //Get all customers with accounts
    [HttpGet("with-accounts")]
    public async Task<ActionResult<List<Customer>>> GetCustomersWithAccounts()
    {
        var customers = await _customerService.GetCustomersWithAccountsAsync();

        var response = _mapper.Map<List<CustomerWithAccountsResponse>>(customers);

        return Ok(response);
    }

    //Get customer by id
    [HttpGet("{id}")] 
    public async Task<ActionResult<Customer>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        var response = _mapper.Map<CustomerResponseDTO>(customer);

        return Ok(response);
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


        _mapper.Map(dto, customer);

        await _customerService.UpdateAsync(customer);

        return Ok("success");
    }


}