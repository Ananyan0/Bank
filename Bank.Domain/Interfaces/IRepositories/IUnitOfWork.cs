
namespace Bank.Domain.Interfaces.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository Accounts { get; }
    ICustomerRepository Customers { get; }

    Task<int> SaveChangesAsync();
}
