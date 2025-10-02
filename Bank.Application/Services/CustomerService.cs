using AutoMapper;
using Bank.Application.DTOs;
using Bank.Application.DTOs.ResponseDTO;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Exceptions;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;

namespace Bank.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> CreateCustomerAsync(Customer customer)
    {
        var isEmailTaken = await _unitOfWork.Customers.GetByEmailAsync(customer.Email);
        if(isEmailTaken != null)
            throw new CustomerException($"Email {customer.Email} is already taken");

        var isPhoneTaken = await _unitOfWork.Customers.GetByPhoneAsync(customer.Phone);
        if(isPhoneTaken != null)
            throw new CustomerException($"Phone number {customer.Phone} is already taken");

        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();


        return customer.Id;
    }


    public async Task<List<CustomerResponseDTO>> GetAllAsync()
    {
        var customers = await _unitOfWork.Customers.GetAllAsync();
        if(customers == null)
            throw new CustomerException("No customers found");

        return _mapper.Map<List<CustomerResponseDTO>>(customers);
    }


    public async Task<Customer> GetByIdAsync(int id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);

        if (customer == null)
            throw new CustomerException($"Customer with id {id} not found");

        return customer;
    }

    public async Task<List<CustomerWithAccountsResponse>> GetCustomersWithAccountsAsync()
    {
        var customers = await _unitOfWork.Customers.GetAllWithIncludeAsync(c => c.Accounts);


        return _mapper.Map<List<CustomerWithAccountsResponse>>(customers);

        //return customers
        //    .Where(c => c.Accounts.Any())
        //    .Select(c => new Customer
        //    {
        //        Id = c.Id,
        //        Name = c.Name,
        //        Email = c.Email,
        //        PhoneNumber = c.PhoneNumber,
        //        Accounts = c.Accounts.Select(a => new Account
        //        {
        //            AccountNumber = a.AccountNumber,
        //            AccountName = a.AccountName,
        //            Balance = a.Balance
        //        }).ToList()
        //    })
        //    .ToList();
    }


    public async Task DeleteCustomerAsync(int customerId)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
            throw new CustomerException("Customer not found");

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

        customer.FirstName = dto.FirstName;
        customer.LastName = dto.LastName;
        customer.Email = dto.Email;
        customer.Phone = dto.Phone;

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
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.Phone,
            Profile = customer.Profile is null ? null : new CustomerProfileResponse
            {
                Address = customer.Profile.Address,
                PassportNumber = customer.Profile.PassportNumber,
                DateOfBirth = customer.Profile.DateOfBirth
            }
        };

    }

    public async Task DeleteAllCustomersAsync()
    {
        var allCustomers = await _unitOfWork.Customers.GetAllAsync();

        if (allCustomers.Any())
        {
            await _unitOfWork.Customers.RemoveRangeAsync(allCustomers);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
