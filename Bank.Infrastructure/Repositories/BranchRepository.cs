using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories;

public class BranchRepository : Repository<Branch>, IBranchRepository
{
    new private readonly AppDbContext _context;

    public BranchRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Branch?> GetByNameAsync(string name)
    {
        return await _context.Branches
            .FirstOrDefaultAsync(b => b.Name == name);
    }
}
