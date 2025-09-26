using AutoMapper;
using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.Exceptions;
using Bank.Application.HelperExtensions;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;

namespace Bank.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> CreateAccountAsync(int customerId, decimal initialBalance)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new ArgumentException($"Customer with id {customerId} not found");
        }

        var account = new Account
        {
            Balance = initialBalance,
            CustomerId = customerId
        };

        await _unitOfWork.Accounts.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();
        return account.Id;
    }

    public async Task<List<Account>> GetAllAsync()
    {
        var account = await _unitOfWork.Accounts.GetAllAsync();
        if(account == null)
               throw new AccountException("No accounts found");

        return account;
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(id);
        if (account == null)
            throw new AccountException($"There is no account with id -> {id}");

        return account;
    }


    public async Task<Account> CreateAccountForCustomerAsync(int customerId, CreateAccountRequest request)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
            throw new CustomerException($"Customer with id -> {customerId} not found");


        var account = _mapper.Map<Account>(request);
        account.CustomerId = customerId;
        account.Balance = 0m;
        account.AccountNumber = AccountExtension.GenerateAccountNumber();


        await _unitOfWork.Accounts.AddAsync(account);
        return account;
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(accountId);
        if (account == null)
            throw new Exception("Account not found");

        await _unitOfWork.Accounts.DeleteAsync(account);
    }
}
