using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank.Application.DTOs;

public record CreateAccountRequest
{
    //public decimal InitialBalance { get; set; }
    [Required]
    [DefaultValue("")]
    public string AccountName { get; set; } = string.Empty;
}
