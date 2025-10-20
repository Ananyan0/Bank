using AutoMapper;
using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Bank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;

    public AccountController(IAccountService _accountService, IMapper mapper, IHttpClientFactory httpClientFactory)
    {
        this._accountService = _accountService;
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
    }

    [HttpGet("check-account/{accountId}")]
    public async Task<IActionResult> CheckAccount(int accountId)
    {
        var account = await _accountService.GetByIdAsync(accountId);

        return Ok(account);
    }



    // Create a new account for a specific customer
    [Authorize(Roles = "Admin")]
    [HttpPost("{customerId}/accounts")]
    public async Task<IActionResult> CreateAccountAsync(int customerId, [FromForm] CreateAccountRequest request)
    {
        var accountId = await _accountService.CreateAccountForCustomerAsync(customerId, request);

        var response = _mapper.Map<AccountResponseDto>(accountId);


        return Ok(response);
    }

    // Delete an account by its ID
    [Authorize(Roles = "Admin")]
    [HttpDelete("accounts/{accountId}")]
    public async Task<IActionResult> DeleteAccountAsync(int accountId)
    {
        var account = await _accountService.GetByIdAsync(accountId);

        await _accountService.DeleteAccountAsync(accountId);

        return Ok($"Success Account with Id -> {accountId} has been deleted.");
    }

    // Get all accounts
    [HttpGet("accounts")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAccountsAsync()
    {
        var accounts = await _accountService.GetAllAsync();
        return Ok(accounts);
    }
}