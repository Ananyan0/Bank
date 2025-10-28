using Bank.Application.DTOs.CreateDTOs;

namespace Bank.Application.Interfaces;

public interface IExchangeRequestService
{
    Task RequestExchange(CreateCurrencyRequest amountUsd);
}
