using Domain.ValueObjects;
using FluentAssertions;

namespace Sales.UnitTests.Domain.ValueObjects;

public class OrderNameTests
{
    [Fact]
    public void OrderName_Of_WithValidName_ShouldCreateOrderName()
    {
        // Arrange
        string validName = "Test Order Name";

        // Act
        var orderName = OrderName.Of(validName);

        // Assert
        orderName.Value.Should().Be(validName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void OrderName_Of_WithEmptyName_ShouldThrowException(string name)
    {
        // Act & Assert
        var action = () => OrderName.Of(name);
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void OrderName_Of_WithLongName_ShouldCreateOrderName()
    {
        // Arrange
        string longName = "This is a very long order name that should be valid for testing purposes";

        // Act
        var orderName = OrderName.Of(longName);

        // Assert
        orderName.Value.Should().Be(longName);
    }

    [Fact]
    public void OrderName_Of_WithSpecialCharacters_ShouldCreateOrderName()
    {
        // Arrange
        string nameWithSpecialChars = "Order #123 - Special Characters!@#$%^&*()";

        // Act
        var orderName = OrderName.Of(nameWithSpecialChars);

        // Assert
        orderName.Value.Should().Be(nameWithSpecialChars);
    }

    [Fact]
    public void OrderName_Of_WithNumbers_ShouldCreateOrderName()
    {
        // Arrange
        string nameWithNumbers = "Order 12345";

        // Act
        var orderName = OrderName.Of(nameWithNumbers);

        // Assert
        orderName.Value.Should().Be(nameWithNumbers);
    }

    [Fact]
    public void OrderName_Of_WithUnicodeCharacters_ShouldCreateOrderName()
    {
        // Arrange
        string nameWithUnicode = "سفارش تست - Test Order";

        // Act
        var orderName = OrderName.Of(nameWithUnicode);

        // Assert
        orderName.Value.Should().Be(nameWithUnicode);
    }
} 