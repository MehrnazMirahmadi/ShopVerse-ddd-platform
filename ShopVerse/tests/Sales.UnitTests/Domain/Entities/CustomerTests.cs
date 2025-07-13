using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;

namespace Sales.UnitTests.Domain.Entities;

public class CustomerTests
{
    [Fact]
    public void Customer_Create_WithValidData_ShouldCreateCustomer()
    {
        // Arrange
        var customerId = CustomerId.Of(Guid.NewGuid());
        string name = "Ali Test";
        string email = "ali@test.com";

        // Act
        var customer = Customer.Create(customerId, name, email);

        // Assert
        customer.Id.Should().Be(customerId);
        customer.Name.Should().Be(name);
        customer.Email.Should().Be(email);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Customer_Create_WithEmptyName_ShouldThrowException(string name)
    {
        // Arrange
        var customerId = CustomerId.Of(Guid.NewGuid());
        string email = "ali@test.com";

        // Act & Assert
        var action = () => Customer.Create(customerId, name, email);
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Customer_Create_WithEmptyEmail_ShouldThrowException(string email)
    {
        // Arrange
        var customerId = CustomerId.Of(Guid.NewGuid());
        string name = "Ali Test";

        // Act & Assert
        var action = () => Customer.Create(customerId, name, email);
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Customer_Create_WithValidEmail_ShouldCreateCustomer()
    {
        // Arrange
        var customerId = CustomerId.Of(Guid.NewGuid());
        string name = "Ali Test";
        string email = "ali.test@example.com";

        // Act
        var customer = Customer.Create(customerId, name, email);

        // Assert
        customer.Email.Should().Be(email);
    }

    [Fact]
    public void Customer_Create_WithLongName_ShouldCreateCustomer()
    {
        // Arrange
        var customerId = CustomerId.Of(Guid.NewGuid());
        string name = "This is a very long customer name that should be valid";
        string email = "ali@test.com";

        // Act
        var customer = Customer.Create(customerId, name, email);

        // Assert
        customer.Name.Should().Be(name);
    }
} 