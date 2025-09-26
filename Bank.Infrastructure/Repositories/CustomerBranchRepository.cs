using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bank.Infrastructure.Repositories;

public class CustomerBranchRepository : Repository<CustomerBranch>, ICustomerBranchRepository
{

    public CustomerBranchRepository(AppDbContext context) : base(context)
    {

    }

    //public async Task AssignCustomerToBranchAsync(int customerId, int branchId)
    //{




    //    var exists = await _context.CustomerBranches
    //        .AnyAsync(cb => cb.CustomerId == customerId && cb.BranchId == branchId);

    //    if (!exists)
    //    {
    //        var customerBranch = new CustomerBranch
    //        {
    //            CustomerId = customerId,
    //            BranchId = branchId
    //        };

    //        await _context.CustomerBranches.AddAsync(customerBranch);
    //        await _context.SaveChangesAsync();
    //    }
    //}

    public async Task<List<Branch>> GetBranchesByCustomerAsync(int customerId)
    {
        return await _context.CustomerBranches
            .Where(cb => cb.CustomerId == customerId)
            .Select(cb => cb.Branch)
            .ToListAsync();
    }

    public async Task<List<Customer>> GetCustomersByBranchAsync(int branchId)
    {
        return await _context.CustomerBranches
            .Where(cb => cb.BranchId == branchId)
            .Select(cb => cb.Customer)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int customerId, int branchId)
    {
        return await _context.CustomerBranches
            .AnyAsync(cb => cb.CustomerId == customerId && cb.BranchId == branchId);
    }
}
