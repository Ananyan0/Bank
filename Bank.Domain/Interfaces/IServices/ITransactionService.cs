using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces.IServices;

public interface ITransactionService
{
    Task<int> CreateTransactionAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
}
