using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
///     Contains unit tests for the Sale entity.
/// </summary>
public class SaleTests
{
    [Fact(DisplayName = "Should initialize CreatedAt and SaleDate on instantiation")]
    public void Given_ValidSale_When_Created_Then_CreatedAtAndSaleDateShouldBeSet()
    {
        // Act
        var sale = SaleTestData.GenerateValidSale();

        // Assert
        Assert.True(sale.CreatedAt <= DateTime.UtcNow);
        Assert.True(sale.SaleDate <= DateTime.UtcNow);
    }

    [Fact(DisplayName = "Should calculate TotalAmount correctly")]
    public void Given_SaleWithItems_When_CalculatingTotal_Then_TotalShouldBeSumOfItems()
    {
        // Arrange
        var total = 200m;
        var items = SaleTestData.GenerateSaleItemsWithTotal(total);
        var sale = new Sale(
            "S123",
            Guid.NewGuid(),
            Guid.NewGuid(),
            items,
            CustomerTestData.GenerateValidCustomer());

        // Act & Assert
        Assert.Equal(total, sale.TotalAmount);
    }

    [Fact(DisplayName = "Should cancel sale and set UpdatedAt")]
    public void Given_ValidSale_When_CancelSale_Then_IsCancelledShouldBeTrueAndUpdatedAtSet()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        sale.CancelSale();

        // Assert
        Assert.True(sale.IsCancelled);
        Assert.NotNull(sale.UpdatedAt);
        Assert.True(sale.UpdatedAt <= DateTime.UtcNow);
    }

    [Fact(DisplayName = "Should assign sale number, customer and branch")]
    public void Given_ValidSale_When_Created_Then_PropertiesShouldBeCorrect()
    {
        var sale = SaleTestData.GenerateValidSale();

        Assert.False(string.IsNullOrWhiteSpace(sale.SaleNumber));
        Assert.NotEqual(Guid.Empty, sale.CustomerId);
        Assert.NotEqual(Guid.Empty, sale.BranchId);
        Assert.NotNull(sale.Customer);
        Assert.NotNull(sale.Branch);
        Assert.NotEmpty(sale.Items);
    }
}