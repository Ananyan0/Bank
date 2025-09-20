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
}
