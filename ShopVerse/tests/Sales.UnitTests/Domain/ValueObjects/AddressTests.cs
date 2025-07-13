using Domain.ValueObjects;
using FluentAssertions;

namespace Sales.UnitTests.Domain.ValueObjects;

public class AddressTests
{
    [Fact]
    public void Address_Of_WithValidData_ShouldCreateAddress()
    {
        // Arrange
        string firstName = "Ali";
        string lastName = "Test";
        string emailAddress = "ali@test.com";
        string addressLine = "123 Main Street";
        string country = "Iran";
        string state = "Tehran";
        string zipCode = "12345";

        // Act
        var address = Address.Of(firstName, lastName, emailAddress, addressLine, country, state, zipCode);

        // Assert
        address.FirstName.Should().Be(firstName);
        address.LastName.Should().Be(lastName);
        address.EmailAddress.Should().Be(emailAddress);
        address.AddressLine.Should().Be(addressLine);
        address.Country.Should().Be(country);
        address.State.Should().Be(state);
        address.ZipCode.Should().Be(zipCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Address_Of_WithEmptyEmailAddress_ShouldThrowException(string emailAddress)
    {
        // Arrange
        string firstName = "Ali";
        string lastName = "Test";
        string addressLine = "123 Main Street";
        string country = "Iran";
        string state = "Tehran";
        string zipCode = "12345";

        // Act & Assert
        var action = () => Address.Of(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
        action.Should().Throw<ArgumentException>();
    }
} 