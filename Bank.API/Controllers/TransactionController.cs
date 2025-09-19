using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // POST api/transaction/deposit
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromForm] CreateTransactionRequest request)
        {
            try
            {
                var transactionId = await _transactionService.DepositAsync(request.AccountId, request.Amount);
                return Ok(new { TransactionId = transactionId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/transaction/withdraw
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromForm] CreateTransactionRequest request)
        {
            try
            {
                var transactionId = await _transactionService.WithdrawAsync(request.AccountId, request.Amount);
                return Ok(new { TransactionId = transactionId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/transaction/transfer
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromForm] TransferTransactionRequest request)
        {
            try
            {
                var transactionId = await _transactionService.TransferAsync(request.FromAccountId, request.ToAccountId, request.Amount);
                return Ok(new { TransactionId = transactionId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/transaction/account/1
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetTransactionsByAccount(int accountId)
        {
            var transactions = await _transactionService.GetTransactionsByAccountAsync(accountId);
            return Ok(transactions);
        }
    }
}
