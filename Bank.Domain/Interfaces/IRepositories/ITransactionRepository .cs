using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces.IRepositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<List<Transaction>> GetByAccountIdAsync(int accountId);

}