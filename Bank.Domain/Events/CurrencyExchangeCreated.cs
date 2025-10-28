namespace Bank.Domain.Events;

public record CurrencyExchangeCreated(
    int ExchangeId,
    decimal AmountUsd,
    decimal RateUsdToAmd,
    decimal ResultAmd,
    DateTime TimestampUtc
);
