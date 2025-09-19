namespace Bank.Application.DTOs.ResponseDTOs;

public record AccountTransResponse
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public string AccountName { get; set; } = null!;
    public decimal Balance { get; set; }
}
