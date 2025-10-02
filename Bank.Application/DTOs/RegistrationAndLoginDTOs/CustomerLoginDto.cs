using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank.Application.DTOs.RegistrationAndLoginDTOs;

public class CustomerLoginDto
{
    [Required]
    [DefaultValue("")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DefaultValue("")]
    public string Password { get; set; } = string.Empty;
}
