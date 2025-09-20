using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces.IRepositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetWithProfileAsync(int id);
    Task RemoveRangeAsync(IEnumerable<Customer> customers);

}
