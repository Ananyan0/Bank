using AutoMapper;
using Bank.Application.DTOs;
using Bank.Application.Exceptions;
using Bank.Application.Interfaces.IServices;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;

namespace Bank.Application.Services;
public class CustomerProfileService : ICustomerProfileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerProfileService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerProfile?> CreateProfileAsync(CreateCustomerProfileRequest request)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
        if (customer == null)
            throw new CustomerException($"Customer with id -> {request.CustomerId} not found");

        if (customer.Profile != null) return customer.Profile;

        var profile = _mapper.Map<CustomerProfile>(request);


        await _unitOfWork.CustomerProfiles.AddAsync(profile);
        await _unitOfWork.SaveChangesAsync();

        return profile;
    }

    public async Task<CustomerProfile?> GetProfileByCustomerIdAsync(int customerId)
    {
        var profile = await _unitOfWork.CustomerProfiles.FindAsync(p => p.Customer.Id == customerId);

        return profile;
    }

    public async Task DeleteProfileAsync(int customerId)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
        if (customer == null)
            throw new CustomerException($"Customer with id -> {customerId} not found");

        var profile = await _unitOfWork.CustomerProfiles.FindAsync(p => p.CustomerId == customerId);
        if (profile != null)
        {
            await _unitOfWork.CustomerProfiles.DeleteAsync(profile);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
