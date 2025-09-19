
namespace Bank.Domain.Interfaces.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository Accounts { get; }
    ICustomerRepository Customers { get; }
    ITransactionRepository Transactions { get; }
    ICustomerProfileRepository CustomerProfiles { get; }

    IBranchRepository Branches { get; }

    ICustomerBranchRepository CustomerBranches { get; }
    Task<int> CompleteAsync(); // ✅ commit changes

    Task<int> SaveChangesAsync();
}