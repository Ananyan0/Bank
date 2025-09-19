using Bank.Application.Interfaces.IServices;
using Bank.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerBranchController : ControllerBase
{
    private readonly ICustomerBranchService _customerBranchService;

    public CustomerBranchController(ICustomerBranchService customerBranchService)
    {
        _customerBranchService = customerBranchService;
    }

    /// <summary>
    /// Привязать клиента к филиалу (создать связь M:N)
    /// </summary>
    [HttpPost("assign")]
    public async Task<IActionResult> AssignCustomerToBranch(int customerId, int branchId)
    {
        await _customerBranchService.AssignCustomerToBranchAsync(customerId, branchId);
        return Ok(new { Message = $"Customer {customerId} assigned to Branch {branchId}" });
    }

    /// <summary>
    /// Получить список филиалов клиента
    /// </summary>
    [HttpGet("customer/{customerId}/branches")]
    public async Task<IActionResult> GetBranchesByCustomer(int customerId)
    {
        var branches = await _customerBranchService.GetBranchesByCustomerAsync(customerId);
        return Ok(branches);
    }

    /// <summary>
    /// Получить список клиентов в филиале
    /// </summary>
    [HttpGet("branch/{branchId}/customers")]
    public async Task<IActionResult> GetCustomersByBranch(int branchId)
    {
        var customers = await _customerBranchService.GetCustomersByBranchAsync(branchId);
        return Ok(customers);
    }
}
