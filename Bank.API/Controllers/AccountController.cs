using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService _accountService)
    {
        this._accountService = _accountService;
    }


    [HttpPost("{customerId}/accounts")]
    public async Task<IActionResult> CreateAccountAsync(int customerId, [FromForm] CreateAccountRequest request)
    {
        var accountId = await _accountService.CreateAccountForCustomerAsync(customerId, request.AccountName);
        return Ok(new { AccountId = accountId });
    }
}