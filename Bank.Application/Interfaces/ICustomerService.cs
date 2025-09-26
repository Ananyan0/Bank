using Bank.Application.DTOs.ResponseDTOs;
using Bank.Domain.Entities;

namespace Bank.Application.Interfaces.IServices;

public interface ICustomerService
{
    Task<int> CreateCustomerAsync(Customer customer);
    Task<List<CustomerResponseDTO>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
    Task<List<CustomerWithAccountsResponse>> GetCustomersWithAccountsAsync();
    Task DeleteCustomerAsync(int id);

    Task UpdateAsync(Customer customer);

    Task<CustomerWithProfileResponse?> GetCustomerWithProfileAsync(int id);
    Task DeleteAllCustomersAsync();

}