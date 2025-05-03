using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
///     Contains unit tests for the Product entity.
///     Tests creation, updates, stock operations, and validations.
/// </summary>
public class ProductTests
{
    [Fact(DisplayName = "Should initialize CreatedAt on instantiation")]
    public void Given_NewProduct_When_Created_Then_CreatedAtShouldBeSet()
    {
        // Act
        var product = ProductTestData.GenerateValidProduct();

        // Assert
        Assert.True(product.CreatedAt <= DateTime.UtcNow);
    }

    [Fact(DisplayName = "Should update product details and set UpdatedAt")]
    public void Given_ValidProduct_When_UpdateProductInfo_Then_PropertiesAndUpdatedAtShouldChange()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        var newName = "Updated Product";
        var newDescription = "Updated Description";
        var newPrice = 999.99m;
        var newStock = 42;

        // Act
        product.UpdateProductInfo(newName, newDescription, newPrice, newStock);

        // Assert
        Assert.Equal(newName, product.Name);
        Assert.Equal(newDescription, product.Description);
        Assert.Equal(newPrice, product.Price);
        Assert.Equal(newStock, product.StockQuantity);
        Assert.NotNull(product.UpdatedAt);
        Assert.True(product.UpdatedAt <= DateTime.UtcNow);
    }

    [Fact(DisplayName = "Should reduce stock correctly when enough stock is available")]
    public void Given_SufficientStock_When_ReduceStock_Then_QuantityShouldDecreaseAndSetUpdatedAt()
    {
        // Arrange
        var product = new Product("Test", "Test Desc", 100, 10);

        // Act
        product.ReduceStock(3);

        // Assert
        Assert.Equal(7, product.StockQuantity);
        Assert.NotNull(product.UpdatedAt);
    }

    [Fact(DisplayName = "Should throw exception when reducing more stock than available")]
    public void Given_InsufficientStock_When_ReduceStock_Then_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var product = new Product("Test", "Test Desc", 100, 5);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => product.ReduceStock(10));
    }

    [Fact(DisplayName = "Should assign valid name, description, price, and stock")]
    public void Given_ValidProduct_When_Created_Then_PropertiesShouldBeCorrect()
    {
        var product = ProductTestData.GenerateValidProduct();

        Assert.False(string.IsNullOrWhiteSpace(product.Name));
        Assert.False(string.IsNullOrWhiteSpace(product.Description));
        Assert.True(product.Price > 0);
        Assert.True(product.StockQuantity > 0);
    }
}