using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    //private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context): base(context)
    {
        //_context = context;
    }

    //public async Task<Customer> GetByIdAsync(int id)
    //    => await _context.Customers.FindAsync(id);

    //public async Task<List<Customer>> GetAllAsync()
    //    => await _context.Customers.Include(c => c.Accounts).ToListAsync();
    //public async Task AddAsync(Customer customer)
    //{
    //    _context.Customers.Add(customer);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task DeleteCustomerAsync(Customer customer)
    //{
    //    _context.Customers.Remove(customer);
    //    await _context.SaveChangesAsync();
    //}
}