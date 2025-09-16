using System.ComponentModel.DataAnnotations;

namespace Bank.Domain.Entities;

public class Transaction : EntityBase
{
    [Key]
    public int TransactionId { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;

    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
}

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}

