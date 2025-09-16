using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Domain.Interfaces.IServices;
using IAccountRepository = Bank.Domain.Interfaces.IRepositories.IAccountRepository;

namespace Bank.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /*    public async Task<int> CreateCustomerAsync(string customerName)
        {
            var customer = new Customer();
            customer.Name = customerName;
            await _customerRepository.AddAsync(customer);
            return customer.Id;
        }*/

    /*    public async Task<int> CreateCustomerWithAccountAsync(string customerName, decimal initialBalance)
        {
            var customer = new Customer { Name = customerName };
            await _customerRepository.AddAsync(customer);

            var account = new Account { Balance = initialBalance, CustomerId = customer.Id };
            await _accountRepository.AddAsync(account);

            return customer.Id;
        }*/

    public async Task<int> CreateCustomerAsync(string name, string email, string phone)
    {
        var customer = new Customer
        {
            Name = name,
            Email = email,
            PhoneNumber = phone
        };

        await _unitOfWork.Customers.AddAsync(customer);

        return customer.Id;
    }


    public async Task<List<Customer>> GetAllAsync()
    {
        var customers = await _unitOfWork.Customers.GetAllAsync();

        return customers.Select(c => new Customer
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber
        }).ToList();
    }



    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Customers.GetByIdAsync(id);
    }

    public async Task<List<Customer>> GetCustomersWithAccountsAsync()
    {
        var customers = await _unitOfWork.Customers.GetAllAsync();

        return customers
            .Where(c => c.Accounts != null && c.Accounts.Any())
            .Select(c => new Customer
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Accounts = c.Accounts.Select(a => new Account
                {
                    AccountNumber = a.AccountNumber,
                    AccountName = a.AccountName,
                    Balance = a.Balance
                }).ToList()
            })
            .ToList();
    }

    public async Task DeleteCustomerAsync(int customerId)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
            throw new Exception("Customer not found");

        foreach (var account in customer.Accounts)
        {
            await _unitOfWork.Accounts.DeleteAsync(account);
        }

        await _unitOfWork.Customers.DeleteAsync(customer);
    }
}
