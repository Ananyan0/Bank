using Bank.Application.DTOs.ResponseDTO;

namespace Bank.Application.DTOs.ResponseDTOs;

public record CustomerWithProfileResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }

    public CustomerProfileResponse? Profile { get; set; }
}
