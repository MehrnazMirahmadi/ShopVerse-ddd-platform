using Domain.ValueObjects;
using FluentAssertions;

namespace Sales.UnitTests.Domain.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Money_Create_WithValidAmount_ShouldCreateMoney()
    {
        // Arrange
        decimal amount = 100.50m;
        string currency = "IRR";

        // Act
        var money = new Money(amount, currency);

        // Assert
        money.Amount.Should().Be(amount);
        money.Currency.Should().Be(currency);
    }

    [Fact]
    public void Money_Create_WithDefaultCurrency_ShouldUseIRR()
    {
        // Arrange
        decimal amount = 100.50m;

        // Act
        var money = new Money(amount);

        // Assert
        money.Amount.Should().Be(amount);
        money.Currency.Should().Be("IRR");
    }

    [Fact]
    public void Money_Create_WithNegativeAmount_ShouldThrowException()
    {
        // Arrange
        decimal negativeAmount = -50.00m;

        // Act & Assert
        var action = () => new Money(negativeAmount);
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Amount cannot be negative");
    }

    [Fact]
    public void Money_Of_WithValidAmount_ShouldCreateMoney()
    {
        // Arrange
        decimal amount = 200.75m;

        // Act
        var money = Money.Of(amount);

        // Assert
        money.Amount.Should().Be(amount);
        money.Currency.Should().Be("IRR");
    }

    [Fact]
    public void Money_Addition_WithSameCurrency_ShouldAddCorrectly()
    {
        // Arrange
        var money1 = Money.Of(100.00m);
        var money2 = Money.Of(50.00m);

        // Act
        var result = money1 + money2;

        // Assert
        result.Amount.Should().Be(150.00m);
        result.Currency.Should().Be("IRR");
    }

    [Fact]
    public void Money_Addition_WithDifferentCurrencies_ShouldThrowException()
    {
        // Arrange
        var money1 = new Money(100.00m, "IRR");
        var money2 = new Money(50.00m, "USD");

        // Act & Assert
        var action = () => money1 + money2;
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Currencies must match to perform addition.");
    }

    [Fact]
    public void Money_Multiplication_WithInteger_ShouldMultiplyCorrectly()
    {
        // Arrange
        var money = Money.Of(25.00m);
        int multiplier = 3;

        // Act
        var result = money * multiplier;

        // Assert
        result.Amount.Should().Be(75.00m);
        result.Currency.Should().Be("IRR");
    }
} 