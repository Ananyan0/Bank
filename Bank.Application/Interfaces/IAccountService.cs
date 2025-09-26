using Bank.Application.DTOs.CreateDTOs;
using Bank.Domain.Entities;

namespace Bank.Application.Interfaces.IServices;

public interface IAccountService
{
    Task<Account> CreateAccountForCustomerAsync(int customerId, CreateAccountRequest request);
    Task<List<Account>> GetAllAsync();
    Task<Account?> GetByIdAsync(int id);
    Task DeleteAccountAsync(int accountId);
}

