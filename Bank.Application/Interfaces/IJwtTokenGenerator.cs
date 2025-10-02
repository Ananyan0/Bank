using Bank.Domain.Entities;

namespace Bank.Application.Interfaces;

public interface IJwtTokenGenerator
{

    Task<string> GenerateTokenAsync(Customer customer);

}
