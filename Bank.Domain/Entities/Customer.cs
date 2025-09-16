using System.ComponentModel.DataAnnotations;

namespace Bank.Domain.Entities;

public class Customer : EntityBase
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<Account> Accounts { get; set; } = new();

}
