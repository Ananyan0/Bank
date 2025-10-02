using Bank.Application.Exceptions;
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

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        var customerEmail = await _context.Customers
            .FirstOrDefaultAsync(c => c.Email == email);
        

        return customerEmail;
    }

    public async Task<Customer?> GetByPhoneAsync(string phone)
    {
        var customerPhone = await _context.Customers
            .FirstOrDefaultAsync(c => c.Phone == phone);

        return customerPhone;
    }
}