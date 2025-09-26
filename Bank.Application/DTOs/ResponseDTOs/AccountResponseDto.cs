namespace Bank.Domain.Entities;

public record AccountResponseDto
{
    public int Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}
