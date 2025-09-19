using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public record CreateCustomerRequest
{
    [Required]
    [DefaultValue("")]
    public string Name { get; set; } = default!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [DefaultValue("")]
    public string? Phone { get; set; }
}
