using Bank.Application.Interfaces.MessagingInterface;
using Bank.Domain.Events;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Bank.Application.Services.MessagingService;

public class RabbitPublisher : IRequestPublisher
{
    private readonly string _host;
    private const string ExchangeName = "bank.currency.requests";

    public RabbitPublisher(IConfiguration cfg)
    {
        _host = cfg.GetValue<string>("RabbitMQ:HostName") ?? "localhost";
    }

    public async Task PublishAsync(CurrencyExchangeRequest request)
    {
        var factory = new ConnectionFactory { HostName = _host };

        // Create async connection
        await using var connection = await factory.CreateConnectionAsync();

        // Create async channel
        await using var channel = await connection.CreateChannelAsync();

        // Declare exchange
        await channel.ExchangeDeclareAsync(
            exchange: ExchangeName,
            type: ExchangeType.Direct,
            durable: true
        );

        // Serialize and publish message

        var routingKey = "usd.amd";

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));

        await channel.BasicPublishAsync(
            exchange: ExchangeName,
            routingKey: routingKey,
            mandatory: false,
            body: body
        );
    }
}