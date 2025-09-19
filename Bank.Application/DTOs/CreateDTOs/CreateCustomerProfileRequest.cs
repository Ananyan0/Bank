namespace Bank.Application.DTOs;
public class CreateCustomerProfileRequest
{
    public int CustomerId { get; set; }  
    public string Address { get; set; } = null!;
    public string PassportNumber { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }

}

