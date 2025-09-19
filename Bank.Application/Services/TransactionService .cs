using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> DepositAsync(int accountId, decimal amount)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(accountId)
                      ?? throw new Exception("Account not found");

        account.Balance += amount;

        var transaction = new Transaction
        {
            AccountId = accountId,
            Amount = amount,
            Date = DateTime.UtcNow,
            Type = "Deposit"
        };

        await _unitOfWork.Transactions.AddAsync(transaction);
        await _unitOfWork.Accounts.UpdateAsync(account);
        await _unitOfWork.SaveChangesAsync();
        


        return transaction.Id;

    }

    public async Task<int> WithdrawAsync(int accountId, decimal amount)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(accountId)
                      ?? throw new Exception("Account not found");

        if (account.Balance < amount)
            throw new Exception("Insufficient funds");

        account.Balance -= amount;

        var transaction = new Transaction
        {
            AccountId = accountId,
            Amount = amount,
            Date = DateTime.UtcNow,
            Type = "Withdraw"
        };

        await _unitOfWork.Transactions.AddAsync(transaction);
        await _unitOfWork.Accounts.UpdateAsync(account);

        return transaction.Id;
    }

    public async Task<int> TransferAsync(int fromAccountId, int toAccountId, decimal amount)
    {
        var fromAccount = await _unitOfWork.Accounts.GetByIdAsync(fromAccountId)
                          ?? throw new Exception("Source account not found");

        var toAccount = await _unitOfWork.Accounts.GetByIdAsync(toAccountId)
                        ?? throw new Exception("Target account not found");

        if (fromAccount.Balance < amount)
            throw new Exception("Insufficient funds");

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;

        var transaction = new Transaction
        {
            AccountId = fromAccountId,
            TargetAccountId = toAccountId,
            Amount = amount,
            Date = DateTime.UtcNow,
            Type = "Transfer"
        };

        await _unitOfWork.Transactions.AddAsync(transaction);
        await _unitOfWork.Accounts.UpdateAsync(fromAccount);
        await _unitOfWork.Accounts.UpdateAsync(toAccount);

        return transaction.Id;
    }

    public async Task<List<TransactionResponse>> GetTransactionsByAccountAsync(int accountId)
    {
        var transactions = await _unitOfWork.Transactions.GetByAccountIdAsync(accountId);

        var transactionDtos = transactions.Select(t => new TransactionResponse
        {
            Id = t.Id,
            Amount = t.Amount,
            Date = t.Date,
            Type = t.Type,
            Account = new AccountTransResponse
            {
                Id = t.Account.Id,
                AccountNumber = t.Account.AccountNumber,
                AccountName = t.Account.AccountName,
                Balance = t.Account.Balance
            },
            TargetAccount = t.TargetAccount == null ? null : new AccountTransResponse
            {
                Id = t.TargetAccount.Id,
                AccountNumber = t.TargetAccount.AccountNumber,
                AccountName = t.TargetAccount.AccountName,
                Balance = t.TargetAccount.Balance
            }
        }).ToList();
        return transactionDtos;
    }
}
