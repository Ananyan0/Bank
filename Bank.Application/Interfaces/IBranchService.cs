using Bank.Domain.Entities;

namespace Bank.Application.Interfaces;

public interface IBranchService
{
    Task<IEnumerable<Branch>> GetAllAsync();
}
