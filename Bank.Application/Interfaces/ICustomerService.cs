using Bank.Application.DTOs.ResponseDTOs;
using Bank.Domain.Entities;

namespace Bank.Application.Interfaces.IServices;

public interface ICustomerService
{
    //Task<int> CreateCustomerAsync(string customerName);
    Task<int> CreateCustomerAsync(string customerName, string email, string? phone);
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<List<Customer>> GetCustomersWithAccountsAsync();
    Task DeleteCustomerAsync(int id);

    Task UpdateAsync(Customer customer);

    Task<CustomerWithProfileResponse?> GetCustomerWithProfileAsync(int id);
    Task DeleteAllCustomersAsync();

}