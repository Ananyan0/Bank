using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces.IRepositories
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<Branch?> GetByNameAsync(string name);
    }
}
