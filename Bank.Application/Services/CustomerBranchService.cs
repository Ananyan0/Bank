using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;

namespace Bank.Application.Services;

public class CustomerBranchService : ICustomerBranchService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerBranchService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AssignCustomerToBranchAsync(int customerId, int branchId)
    {
        await _unitOfWork.CustomerBranches.AssignCustomerToBranchAsync(customerId, branchId);
    }

    public async Task<List<Branch>> GetBranchesByCustomerAsync(int customerId)
    {
        return await _unitOfWork.CustomerBranches.GetBranchesByCustomerAsync(customerId);
    }

    public async Task<List<Customer>> GetCustomersByBranchAsync(int branchId)
    {
        return await _unitOfWork.CustomerBranches.GetCustomersByBranchAsync(branchId);
    }

}
