using System.ComponentModel.DataAnnotations;

namespace Bank.Application.DTOs.CreateDTOs;

public class CreateBranchRequest
{
    [Required(ErrorMessage = "Branch name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Branch name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public string Address { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Phone number is not valid.")]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; } = string.Empty;

}
