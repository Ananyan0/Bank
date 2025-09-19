using Bank.Domain.Entities;

namespace Bank.Application.Interfaces.IServices;

public interface ICustomerProfileService
{
    Task<CustomerProfile?> CreateProfileAsync(int customerId, string address, string passportNumber, DateTime dateOfBirth);
    Task<CustomerProfile?> GetProfileByCustomerIdAsync(int customerId);

}
