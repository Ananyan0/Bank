using AutoMapper;
using Bank.Application.DTOs.RegistrationDTO;
using Bank.Application.Exceptions;
using Bank.Application.Interfaces;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace Bank.Application.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<Customer> _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;


    public RegistrationService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<Customer> passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }



    public async Task<string> RegisterCustomerAsync(CustomerRegistrationDto register)
    {

        if (string.IsNullOrWhiteSpace(register.Email))
            throw new CustomerException("Email cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(register.Password))
            throw new CustomerException("Password cannot be null or empty.");


        var customer = await _unitOfWork.Customers.GetByEmailAsync(register.Email);
        if (customer == null)
            throw new CustomerException($"There is no customer with email -> {register.Email}");


        if (!string.IsNullOrEmpty(customer.PasswordHash))
            throw new CustomerException("Customer is already registered");

        customer.PasswordHash = _passwordHasher.HashPassword(customer, register.Password);


        await _unitOfWork.Customers.UpdateAsync(customer);

        return  await _jwtTokenGenerator.GenerateTokenAsync(customer);
    }
}
