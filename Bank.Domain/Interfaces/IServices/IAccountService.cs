using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces.IServices;

public interface IAccountService
{
    Task<int> CreateAccountForCustomerAsync(int customerId, string AccountName);
    Task<List<Account>> GetAllAsync();
    Task<Account?> GetByIdAsync(int id);
    Task DeleteAccountAsync(int accountId);
}

