using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
///     Provides factory methods for generating test data for the Product entity.
/// </summary>
public static class ProductTestData
{
    private static readonly Faker Faker = new();

    public static Product GenerateValidProduct()
    {
        return new Product(
            Faker.Commerce.ProductName(),
            Faker.Commerce.ProductDescription(),
            Faker.Random.Decimal(10, 1000),
            Faker.Random.Int(1, 100));
    }

    public static string GenerateInvalidName()
    {
        return string.Empty;
    }

    public static decimal GenerateInvalidPrice()
    {
        return -1; // Invalid: negative price
    }

    public static int GenerateInvalidStockQuantity()
    {
        return -10; // Invalid: negative stock
    }
}