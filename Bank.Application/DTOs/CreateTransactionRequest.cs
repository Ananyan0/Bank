using Bank.Domain.Entities;

namespace Bank.Application.DTOs;

public record CreateTransactionRequest
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public TransactionType TransactionType { get; set; }

}
