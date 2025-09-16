using System.ComponentModel.DataAnnotations;

namespace Bank.Domain.Entities;

public class Account : EntityBase
{
    [Key]
    public int Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

}
