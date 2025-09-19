using Bank.Application.DTOs;
using Bank.Application.DTOs.ResponseDTO;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;

namespace Bank.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateCustomerAsync(string name, string email, string? phone)
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
        var customers = await _unitOfWork.Customers.GetAllWithIncludeAsync(c => c.Accounts);

        return customers
            .Where(c => c.Accounts.Any())
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


    public async Task UpdateCustomerFromDtoAsync(int id, CustomerUpdateDTO dto)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id)
                      ?? throw new Exception("Customer not found");

        customer.Name = dto.Name;
        customer.Email = dto.Email;
        customer.PhoneNumber = dto.Phone;

        await _unitOfWork.Customers.UpdateAsync(customer);

        await _unitOfWork.SaveChangesAsync();
    }


    public async Task UpdateAsync(Customer customer)
    {
        await _unitOfWork.Customers.UpdateAsync(customer);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<CustomerWithProfileResponse?> GetCustomerWithProfileAsync(int id)
    {
        var customer = await _unitOfWork.Customers.GetWithProfileAsync(id);
        if (customer == null)
            return null;

        return new CustomerWithProfileResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Profile = customer.Profile is null ? null : new CustomerProfileResponse
            {
                Address = customer.Profile.Address,
                PassportNumber = customer.Profile.PassportNumber,
                DateOfBirth = customer.Profile.DateOfBirth
            }
        };

    }
}
