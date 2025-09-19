using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;

namespace Bank.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAccountAsync(int customerId, decimal initialBalance)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new ArgumentException($"Customer with id {customerId} not found");
        }

        var account = new Account
        {
            Balance = initialBalance,
            CustomerId = customerId
        };

        await _unitOfWork.Accounts.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();
        return account.Id;
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _unitOfWork.Accounts.GetAllAsync();
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Accounts.GetByIdAsync(id);
    }


    public async Task<int> CreateAccountForCustomerAsync(int customerId, string AccountName)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
            throw new Exception("Customer not found");

        var account = new Account
        {
            CustomerId = customerId,
            AccountName = AccountName,
        };

        await _unitOfWork.Accounts.AddAsync(account);
        return account.Id;
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(accountId);
        if (account == null)
            throw new Exception("Account not found");

        await _unitOfWork.Accounts.DeleteAsync(account);
    }
}
