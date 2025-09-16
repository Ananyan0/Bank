using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Domain.Interfaces.IServices;

namespace Bank.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<int> CreateTransactionAsync(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        await _transactionRepository.AddAsync(transaction);
        return transaction.AccountId;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
    {
        if (accountId <= 0)
            throw new ArgumentException("AccountId must be greater than zero.", nameof(accountId));

        return await _transactionRepository.GetByAccountIdAsync(accountId);
    }

}
