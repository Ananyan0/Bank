namespace Bank.Application.DTOs.ResponseDTO;

public record CustomerProfileResponse
{
    public string Address { get; set; } = null!;
    public string PassportNumber { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
}
