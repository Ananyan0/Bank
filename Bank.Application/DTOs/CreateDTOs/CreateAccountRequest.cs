using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank.Application.DTOs.CreateDTOs;

public record CreateAccountRequest
{
    [Required]
    [DefaultValue("")]
    public string AccountName { get; set; } = string.Empty;
}
