using Bank.Application.Exceptions;
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

    //public async Task AssignCustomerToBranchAsync(int customerId, int branchId)
    //{
    //    await _unitOfWork.CustomerBranches.AssignCustomerToBranchAsync(customerId, branchId);
    //}

    public async Task<List<Branch>> GetBranchesByCustomerAsync(int customerId)
    {
        var customer = await _unitOfWork.CustomerBranches.GetBranchesByCustomerAsync(customerId);
        if (customer == null)
            throw new CustomerException($"There is not customer with id -> {customerId}");

        return customer;
    }

    public async Task<List<Customer>> GetCustomersByBranchAsync(int branchId)
    {
        var branch = await _unitOfWork.CustomerBranches.GetCustomersByBranchAsync(branchId);
        if (branch == null)
            throw new BranchException($"There is no branch with id -> {branchId}");

        return branch;
    }


    public async Task AssignCustomerToBranchAsync(int customerId, int branchId)
    {
        // Ensure customer exists
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId)
                       ?? throw new CustomerException($"Customer {customerId} not found");

        // Ensure branch exists
        var branch = await _unitOfWork.Branches.GetByIdAsync(branchId)
                    ?? throw new BranchException($"Branch {branchId} not found");

        // Check if assignment already exists
        var exists = await _unitOfWork.CustomerBranches.ExistsAsync(customerId, branchId);
        if (exists)
            throw new CustomerException($"Customer {customerId} is already assigned to branch {branchId}");

        // Assign
        await _unitOfWork.CustomerBranches.AddAsync(new CustomerBranch
        {
            CustomerId = customerId,
            BranchId = branchId
        });

        await _unitOfWork.CompleteAsync();
    }

}