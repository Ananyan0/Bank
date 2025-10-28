using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.Interfaces;
using Bank.Application.Interfaces.MessagingInterface;
using Bank.Domain.Events;

namespace Bank.Application.Services;

public class ExchangeRequestService : IExchangeRequestService
{
    private readonly IRequestPublisher _publisher;
    public ExchangeRequestService(IRequestPublisher publisher) => _publisher = publisher;

    public async Task RequestExchange(CreateCurrencyRequest req)
    {
        var msg = new CurrencyExchangeRequest(req.AmountUsd, DateTime.UtcNow);
        await _publisher.PublishAsync(msg);
    }
}