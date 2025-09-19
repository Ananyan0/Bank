using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{

    public TransactionRepository(AppDbContext context) : base(context)
    {
    }


    public async Task<List<Transaction>> GetByAccountIdAsync(int accountId)
    {
        var query =  _context.Transactions
                             .Where(t => t.AccountId == accountId || t.TargetAccountId == accountId);
        query = query.Include(t => t.Account)
                     .Include(t => t.TargetAccount);
        return await query.ToListAsync();
    }
}