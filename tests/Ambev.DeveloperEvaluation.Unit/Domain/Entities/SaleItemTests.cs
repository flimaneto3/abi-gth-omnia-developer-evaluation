using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
///     Contains unit tests for the SaleItem entity.
/// </summary>
public class SaleItemTests
{
    [Fact(DisplayName = "Should calculate total item price correctly")]
    public void Given_ValidSaleItem_When_TotalItemPrice_Then_ShouldBeQuantityTimesUnitPriceMinusDiscount()
    {
        // Arrange
        var quantity = 3;
        var unitPrice = 100m;
        var discount = 50m;
        var expectedTotal = quantity * unitPrice - discount;

        var item = new SaleItem(Guid.NewGuid(), quantity, unitPrice, discount);

        // Act & Assert
        Assert.Equal(expectedTotal, item.TotalItemPrice);
    }

    [Fact(DisplayName = "Should assign product and product details")]
    public void Given_ValidSaleItem_When_ProductAssigned_Then_ShouldNotBeNull()
    {
        // Arrange
        var item = SaleItemTestData.GenerateValidSaleItem();

        // Assert
        Assert.NotEqual(Guid.Empty, item.ProductId);
        Assert.True(item.Quantity > 0);
        Assert.True(item.UnitPrice > 0);
        Assert.True(item.TotalItemPrice >= 0);
        Assert.NotNull(item.Product);
    }

    [Fact(DisplayName = "Should handle zero discount correctly")]
    public void Given_SaleItemWithZeroDiscount_When_CalculatingTotal_Then_ShouldEqualQuantityTimesPrice()
    {
        // Arrange
        var item = new SaleItem(Guid.NewGuid(), 2, 150m, 0);

        // Act & Assert
        Assert.Equal(300m, item.TotalItemPrice);
    }

    [Fact(DisplayName = "Should allow discount equal to full amount (free item)")]
    public void Given_SaleItemWithFullDiscount_When_CalculatingTotal_Then_ShouldBeZero()
    {
        var item = new SaleItem(Guid.NewGuid(), 1, 100m, 100m);

        Assert.Equal(0m, item.TotalItemPrice);
    }

    [Fact(DisplayName = "Should not throw when discount is greater than total (negative total allowed)")]
    public void Given_SaleItemWithExcessiveDiscount_When_CalculatingTotal_Then_CanBeNegative()
    {
        var item = new SaleItem(Guid.NewGuid(), 1, 50m, 100m);

        Assert.Equal(-50m, item.TotalItemPrice);
    }
}