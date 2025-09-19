using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;

namespace Bank.Infrastructure.Repositories;

public class CustomerProfileRepository : Repository<CustomerProfile>, ICustomerProfileRepository
{
    public CustomerProfileRepository(AppDbContext context) : base(context) { }

}
