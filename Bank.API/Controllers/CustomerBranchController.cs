using AutoMapper;
using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerBranchController : ControllerBase
{
    private readonly ICustomerBranchService _customerBranchService;
    private readonly IMapper _mapper;

    public CustomerBranchController(ICustomerBranchService customerBranchService, IMapper mapper)
    {
        _customerBranchService = customerBranchService;
        _mapper = mapper;
    }


    [Authorize(Roles = "Admin")]
    [HttpPost("assign")]
    public async Task<IActionResult> AssignCustomerToBranch([FromForm] AssignCustomerToBranchRequest request)
    {
        await _customerBranchService.AssignCustomerToBranchAsync(request.CustomerId, request.BranchId);

        return Ok(new { Message = $"Customer {request.CustomerId} assigned to Branch {request.BranchId}" });
    }



    [Authorize(Roles = "Admin")]
    [HttpGet("customer/{customerId}/branches")]
    public async Task<IActionResult> GetBranchesByCustomer(int customerId)
    {
        var branches = await _customerBranchService.GetBranchesByCustomerAsync(customerId);

        var response = _mapper.Map<List<BranchResponseDto>>(branches);

        return Ok(response);
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("branch/{branchId}/customers")]
    public async Task<IActionResult> GetCustomersByBranch(int branchId)
    {
        var customers = await _customerBranchService.GetCustomersByBranchAsync(branchId);

        var response = _mapper.Map<List<CustomerResponseDTO>>(customers);

        return Ok(response);
    }
}
