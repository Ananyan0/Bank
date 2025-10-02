using Bank.Application.DTOs.RegistrationAndLoginDTOs;
using Bank.Application.Exceptions;
using Bank.Application.Interfaces;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace Bank.Application;

public class CustomerLoginService : ICustomerLoginService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Customer> _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public CustomerLoginService(
        IUnitOfWork unitOfWork,
        IPasswordHasher<Customer> passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> LoginAsync(CustomerLoginDto loginDto)
    {
        var customer = await _unitOfWork.Customers.GetByEmailAsync(loginDto.Email);
        if (customer == null)
            throw new CustomerException("Customer with this email does not exist.");

        var result = _passwordHasher.VerifyHashedPassword(customer, customer.PasswordHash, loginDto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new CustomerException("Invalid password.");

        return await _jwtTokenGenerator.GenerateTokenAsync(customer);
    }
}
