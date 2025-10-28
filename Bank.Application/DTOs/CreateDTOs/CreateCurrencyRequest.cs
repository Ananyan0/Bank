namespace Bank.Application.DTOs.CreateDTOs;

public record CreateCurrencyRequest
{
    public decimal AmountUsd { get; set; }
}