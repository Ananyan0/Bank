using Bank.Domain.Events;

namespace Bank.Application.Interfaces.MessagingInterface;

public interface IRequestPublisher
{
    Task PublishAsync(CurrencyExchangeRequest request);
}
