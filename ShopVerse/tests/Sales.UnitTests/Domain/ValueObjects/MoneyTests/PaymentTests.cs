using Domain.ValueObjects;
using FluentAssertions;

namespace Sales.UnitTests.Domain.ValueObjects;

public class PaymentTests
{
    [Fact]
    public void Payment_Of_WithValidData_ShouldCreatePayment()
    {
        // Arrange
        string cardName = "Test Card";
        string cardNumber = "1234567890123456";
        string expiration = "12/26";
        string cvv = "123";
        int paymentMethod = 1;

        // Act
        var payment = Payment.Of(cardName, cardNumber, expiration, cvv, paymentMethod);

        // Assert
        payment.CardName.Should().Be(cardName);
        payment.CardNumber.Should().Be(cardNumber);
        payment.Expiration.Should().Be(expiration);
        payment.CVV.Should().Be(cvv);
        payment.PaymentMethod.Should().Be(paymentMethod);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Payment_Of_WithEmptyCardName_ShouldThrowException(string cardName)
    {
        // Arrange
        string cardNumber = "1234567890123456";
        string expiration = "12/26";
        string cvv = "123";
        int paymentMethod = 1;

        // Act & Assert
        var action = () => Payment.Of(cardName, cardNumber, expiration, cvv, paymentMethod);
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Payment_Of_WithEmptyCardNumber_ShouldThrowException(string cardNumber)
    {
        // Arrange
        string cardName = "Test Card";
        string expiration = "12/26";
        string cvv = "123";
        int paymentMethod = 1;

        // Act & Assert
        var action = () => Payment.Of(cardName, cardNumber, expiration, cvv, paymentMethod);
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Payment_Of_WithEmptyCVV_ShouldThrowException(string cvv)
    {
        // Arrange
        string cardName = "Test Card";
        string cardNumber = "1234567890123456";
        string expiration = "12/26";
        int paymentMethod = 1;

        // Act & Assert
        var action = () => Payment.Of(cardName, cardNumber, expiration, cvv, paymentMethod);
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("1234")] // Too long
    [InlineData("12")]   // Too short
    public void Payment_Of_WithInvalidCVVLength_ShouldThrowException(string cvv)
    {
        // Arrange
        string cardName = "Test Card";
        string cardNumber = "1234567890123456";
        string expiration = "12/26";
        int paymentMethod = 1;

        // Act & Assert
        var action = () => Payment.Of(cardName, cardNumber, expiration, cvv, paymentMethod);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Payment_Of_WithValidCVV_ShouldCreatePayment()
    {
        // Arrange
        string cardName = "Test Card";
        string cardNumber = "1234567890123456";
        string expiration = "12/26";
        string cvv = "123"; // Valid 3-digit CVV
        int paymentMethod = 1;

        // Act
        var payment = Payment.Of(cardName, cardNumber, expiration, cvv, paymentMethod);

        // Assert
        payment.CVV.Should().Be(cvv);
    }
}