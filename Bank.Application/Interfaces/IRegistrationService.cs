using Bank.Application.DTOs.RegistrationDTO;

namespace Bank.Application.Interfaces;

public interface IRegistrationService
{
    Task<string> RegisterCustomerAsync(CustomerRegistrationDto register);
}
