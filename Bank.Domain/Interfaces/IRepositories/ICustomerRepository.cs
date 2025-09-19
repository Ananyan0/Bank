using Bank.Domain.Entities;
using System.Linq.Expressions;

namespace Bank.Domain.Interfaces.IRepositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetWithProfileAsync(int id);
}
