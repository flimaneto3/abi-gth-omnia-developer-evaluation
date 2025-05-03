using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
///     Provides factory methods for generating test data for the SaleItem entity.
/// </summary>
public static class SaleItemTestData
{
    private static readonly Faker Faker = new();

    public static SaleItem GenerateValidSaleItem()
    {
        return new SaleItem(
            Guid.NewGuid(),
            Faker.Random.Int(1, 10),
            Faker.Random.Decimal(10, 500),
            Faker.Random.Decimal(0, 50))
        {
            Product = ProductTestData.GenerateValidProduct()
        };
    }

    public static int GenerateInvalidQuantity()
    {
        return -1;
    }

    public static decimal GenerateInvalidPrice()
    {
        return -10;
    }

    public static decimal GenerateInvalidDiscount()
    {
        return -5;
    }
}