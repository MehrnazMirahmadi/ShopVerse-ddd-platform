namespace Domain.ValueObjects;
public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money() { }

    public Money(decimal amount, string currency = "IRR")
    {
        if (amount < 0) throw new InvalidOperationException("Amount cannot be negative");
        Amount = amount;
        Currency = currency;
    }

    public static Money Of(decimal amount, string currency = "IRR")
    {
        return new Money(amount, currency);
    }
}

