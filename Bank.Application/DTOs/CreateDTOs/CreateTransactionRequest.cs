namespace Bank.Application.DTOs.CreateDTOs;
public record CreateTransactionRequest
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;

}
