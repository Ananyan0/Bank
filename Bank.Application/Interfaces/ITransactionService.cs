//using Bank.Application.DTOs.ResponseDTOs;

using Bank.Application.DTOs.ResponseDTOs;

namespace Bank.Application.Interfaces.IServices;

public interface ITransactionService
{
    Task<int> DepositAsync(int accountId, decimal amount);
    Task<int> WithdrawAsync(int accountId, decimal amount);
    Task<int> TransferAsync(int fromAccountId, int toAccountId, decimal amount);
    Task<List<TransactionResponse>> GetTransactionsByAccountAsync(int accountId);
}
