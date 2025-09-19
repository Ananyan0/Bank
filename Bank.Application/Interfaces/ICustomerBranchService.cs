using Bank.Domain.Entities;

namespace Bank.Application.Interfaces.IServices
{
    public interface ICustomerBranchService
    {
        Task AssignCustomerToBranchAsync(int customerId, int branchId);
        Task<List<Branch>> GetBranchesByCustomerAsync(int customerId);
        Task<List<Customer>> GetCustomersByBranchAsync(int branchId);
    }
}
