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


    // Create a new account for a specific customer
    [HttpPost("{customerId}/accounts")]
    public async Task<IActionResult> CreateAccountAsync(int customerId, [FromForm] CreateAccountRequest request)
    {
        var accountId = await _accountService.CreateAccountForCustomerAsync(customerId, request.AccountName);
        return Ok(new { AccountId = accountId });
    }

    // Delete an account by its ID
    [HttpDelete("accounts/{accountId}")]
    public async Task<IActionResult> DeleteAccountAsync(int accountId)
    {
        try
        {
            await _accountService.DeleteAccountAsync(accountId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Account with Id {accountId} not found.");
        }
    }

    // Get all accounts
    [HttpGet("accounts")]
    public async Task<IActionResult> GetAllAccountsAsync()
    {
        var accounts = await _accountService.GetAllAsync();
        return Ok(accounts);
    }
}