using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces.IRepositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
    Task AddAsync(Customer customer);
    Task<List<Customer>> GetAllAsync();
    Task DeleteAsync(Customer customer);

}
