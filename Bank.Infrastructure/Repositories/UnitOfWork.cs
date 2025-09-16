using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;

        public UnitOfWork(AppDbContext context, Lazy<ICustomerRepository> customerRepository, Lazy<IAccountRepository> accountRepository)
        {
            _context = context;

            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        public ICustomerRepository Customers => _customerRepository.Value;
        public IAccountRepository Accounts => _accountRepository.Value; 
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
