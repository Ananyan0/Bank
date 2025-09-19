namespace Bank.Application.DTOs.ResponseDTOs;

public record TransactionResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; } = null!;

    public AccountTransResponse Account { get; set; } = null!;
    public AccountTransResponse? TargetAccount { get; set; }
}
