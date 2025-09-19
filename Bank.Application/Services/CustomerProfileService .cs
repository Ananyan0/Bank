using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;

namespace Bank.Application.Services;
public class CustomerProfileService : ICustomerProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerProfileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Domain метод теперь использует отдельные параметры, а не DTO
    public async Task<CustomerProfile?> CreateProfileAsync(int customerId, string address, string passportNumber, DateTime dateOfBirth)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null) return null;

        if (customer.Profile != null) return customer.Profile;

        var profile = new CustomerProfile
        {
            Address = address,
            PassportNumber = passportNumber,
            DateOfBirth = dateOfBirth,
            CustomerId = customer.Id
        };

        await _unitOfWork.CustomerProfiles.AddAsync(profile);
        await _unitOfWork.SaveChangesAsync();

        return profile;
    }

    public async Task<CustomerProfile?> GetProfileByCustomerIdAsync(int customerId)
    {
        var profile = await _unitOfWork.CustomerProfiles.FindAsync(p => p.Customer.Id == customerId);

        return profile;
    }
}
