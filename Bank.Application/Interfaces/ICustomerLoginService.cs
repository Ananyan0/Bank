using Bank.Application.DTOs.RegistrationAndLoginDTOs;

namespace Bank.Application.Interfaces;

public interface ICustomerLoginService
{
    Task<string> LoginAsync(CustomerLoginDto loginDto);

}
