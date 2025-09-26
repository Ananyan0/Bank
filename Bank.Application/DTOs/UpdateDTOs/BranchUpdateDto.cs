using System.ComponentModel.DataAnnotations;

namespace Bank.Application.DTOs;

public record BranchUpdateDto
{

    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty!;

    [StringLength(20, MinimumLength = 6, ErrorMessage = "Phone must be between 2 and 100 characters.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string Phone { get; set; } = string.Empty!;

    public string Address { get; set; } = string.Empty; 
}
