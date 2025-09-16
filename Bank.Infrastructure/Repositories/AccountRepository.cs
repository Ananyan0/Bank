using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{

    public AccountRepository(AppDbContext context) : base(context) 
    { 
    }

    //public async Task AddAsync(Account account)
    //{
    //    _context.Accounts.Add(account);
    //    await _context.SaveChangesAsync();
    //}


    //public async Task DeleteAsync(Account account)
    //{
    //    _context.Accounts.Remove(account);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task<List<Account>> GetAllAsync()
    //{
    //    return await _context.Accounts.Include(a => a.Customer).ToListAsync();
    //}

    //public async Task<Account?> GetByIdAsync(int id)
    //{
    //    return await _context.Accounts
    //        .Include(a => a.Customer)
    //        .FirstOrDefaultAsync(a => a.Id == id);
    //}
}
