using System.ComponentModel.DataAnnotations;

namespace Bank.Application.DTOs;

public record CustomerUpdateDTO
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last Name is required.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;

    [StringLength(20, MinimumLength = 6, ErrorMessage = "Phone must be between 2 and 100 characters.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string Phone { get; set; } = null!;
}
