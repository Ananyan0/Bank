using Bank.Application.DTOs;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateTransactionRequest request)
    {
        var transaction = new Transaction
        {
            AccountId = request.AccountId,
            Amount = request.Amount,
            TransactionType = request.TransactionType,
            Timestamp = DateTime.UtcNow,
            Description = request.Description
        };

        var id = await _transactionService.CreateTransactionAsync(transaction);
        return Ok(new { TransactionId = id });
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetByAccount(int accountId)
    {
        var transactions = await _transactionService.GetTransactionsByAccountIdAsync(accountId);
        return Ok(transactions);
    }
}
