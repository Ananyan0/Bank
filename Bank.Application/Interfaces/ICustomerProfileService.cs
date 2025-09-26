using Bank.Application.DTOs;
using Bank.Domain.Entities;

namespace Bank.Application.Interfaces.IServices;

public interface ICustomerProfileService
{
    Task<CustomerProfile?> CreateProfileAsync(CreateCustomerProfileRequest request);
    Task<CustomerProfile?> GetProfileByCustomerIdAsync(int customerId);
    Task DeleteProfileAsync(int customerId);
}
