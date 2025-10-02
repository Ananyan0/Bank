using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public record CreateCustomerRequest
{
    [Required]
    [DefaultValue("")]
    public string FirstName { get; set; } = default!;

    [Required]
    [DefaultValue("")]
    public string LastName { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [Phone]
    [DefaultValue("")]
    public string? Phone { get; set; }
}
