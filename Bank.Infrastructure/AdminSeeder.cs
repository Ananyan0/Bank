using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace Bank.Infrastructure;

public class AdminSeeder
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Customer> _passwordHasher;

    public AdminSeeder(IUnitOfWork unitOfWork, IPasswordHasher<Customer> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync()
    {
        var adminEmail = "ananyan2020@gmail.com";
        var admin = await _unitOfWork.Customers.GetByEmailAsync(adminEmail);


        if (admin == null)
        {
            admin = new Customer
            {
                Email = adminEmail,
                FirstName = "Admin",
                Role = "Admin"
            };

            admin.PasswordHash = _passwordHasher.HashPassword(admin, "ananyan2020");
            await _unitOfWork.Customers.AddAsync(admin);
            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            admin.Role = "Admin";
            await _unitOfWork.Customers.UpdateAsync(admin);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}