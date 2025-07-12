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

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Currencies must match to perform addition.");

        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator *(Money money, int multiplier)
    {
        return new Money(money.Amount * multiplier, money.Currency);
    }
}
