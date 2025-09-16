using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId)
    {
        return await _context.Transactions
                             .Where(t => t.AccountId == accountId)
                             .ToListAsync();
    }
}