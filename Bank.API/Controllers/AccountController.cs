using AutoMapper;
using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountController(IAccountService _accountService, IMapper mapper)
    {
        this._accountService = _accountService;
        _mapper = mapper;
    }


    // Create a new account for a specific customer
    [HttpPost("{customerId}/accounts")]
    public async Task<IActionResult> CreateAccountAsync(int customerId, [FromForm] CreateAccountRequest request)
    {
        var accountId = await _accountService.CreateAccountForCustomerAsync(customerId, request);

        var response = _mapper.Map<AccountResponseDto>(accountId);


        return Ok(response);
    }

    // Delete an account by its ID
    [HttpDelete("accounts/{accountId}")]
    public async Task<IActionResult> DeleteAccountAsync(int accountId)
    {

        var account = await _accountService.GetByIdAsync(accountId);

        await _accountService.DeleteAccountAsync(accountId);

        return Ok($"Success Account with Id -> {accountId} has been deleted.");
    }

    // Get all accounts
    [HttpGet("accounts")]
    public async Task<IActionResult> GetAllAccountsAsync()
    {
        var accounts = await _accountService.GetAllAsync();
        return Ok(accounts);
    }
}