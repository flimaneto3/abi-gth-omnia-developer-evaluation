using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
///     Contains unit tests for the Customer entity.
///     Tests construction, property updates, and timestamp behavior.
/// </summary>
public class CustomerTests
{
    [Fact(DisplayName = "Should initialize CreatedAt on instantiation")]
    public void Given_NewCustomer_When_Created_Then_CreatedAtShouldBeSet()
    {
        // Act
        var customer = CustomerTestData.GenerateValidCustomer();

        // Assert
        Assert.True(customer.CreatedAt <= DateTime.UtcNow);
    }

    [Fact(DisplayName = "Should update email and phone and set UpdatedAt")]
    public void Given_ValidCustomer_When_UpdateContactInfo_Then_EmailPhoneAndUpdatedAtShouldBeSet()
    {
        // Arrange
        var customer = CustomerTestData.GenerateValidCustomer();
        var newEmail = "new.email@example.com";
        var newPhone = "+5511999998888";

        // Act
        customer.UpdateContactInfo(newEmail, newPhone);

        // Assert
        Assert.Equal(newEmail, customer.Email);
        Assert.Equal(newPhone, customer.Phone);
        Assert.NotNull(customer.UpdatedAt);
        Assert.True(customer.UpdatedAt <= DateTime.UtcNow);
    }

    [Fact(DisplayName = "Should assign valid name, email and phone")]
    public void Given_ValidCustomer_When_Created_Then_PropertiesShouldBeCorrect()
    {
        // Arrange
        var customer = CustomerTestData.GenerateValidCustomer();

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(customer.Name));
        Assert.Contains("@", customer.Email);
        Assert.StartsWith("+55", customer.Phone);
    }
}