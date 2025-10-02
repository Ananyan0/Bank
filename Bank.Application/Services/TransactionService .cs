using AutoMapper;
using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TransactionService(IUnitOfWork unitOfWork)
    {
    }

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TransactionResponse> DepositAsync(CreateTransactionRequest request)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(request.AccountId)
                      ?? throw new Exception("Account not found");

        account.Balance += request.Amount;
        await _unitOfWork.Accounts.UpdateAsync(account);


        var transaction = _mapper.Map<Transaction>(request);
        transaction.AccountId = account.Id;

        await _unitOfWork.Transactions.AddAsync(transaction);
        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<TransactionResponse>(transaction);


        return response;

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

        return _mapper.Map<List<TransactionResponse>>(transactions);
    }
}
