using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces.IRepositories;

public interface ICustomerBranchRepository : IRepository<CustomerBranch>
{
    //Task AssignCustomerToBranchAsync(int customerId, int branchId);
    Task<List<Branch>> GetBranchesByCustomerAsync(int customerId);
    Task<List<Customer>> GetCustomersByBranchAsync(int branchId);
    Task<bool> ExistsAsync(int customerId, int branchId);
}
