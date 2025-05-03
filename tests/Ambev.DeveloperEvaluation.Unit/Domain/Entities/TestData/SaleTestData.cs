using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
///     Provides factory methods for generating test data for the Sale entity.
/// </summary>
public static class SaleTestData
{
    private static readonly Faker Faker = new();

    public static Sale GenerateValidSale()
    {
        var customer = CustomerTestData.GenerateValidCustomer();
        var branch = BranchTestData.GenerateValidBranch();

        customer.Id = Guid.NewGuid();
        branch.Id = Guid.NewGuid();

        var items = new List<SaleItem>
        {
            new(Guid.NewGuid(), 2, 50, 0),
            new(Guid.NewGuid(), 1, 100, 0)
        };

        return new Sale(
            Faker.Random.AlphaNumeric(10),
            customer.Id,
            branch.Id,
            items,
            customer)
        {
            Branch = branch
        };
    }

    public static List<SaleItem> GenerateSaleItemsWithTotal(decimal total)
    {
        // Creates two items that sum up to the total
        return new List<SaleItem>
        {
            new(Guid.NewGuid(), 1, total / 2, 0),
            new(Guid.NewGuid(), 1, total / 2, 0)
        };
    }

    public static string GenerateInvalidSaleNumber()
    {
        return ""; // Empty = invalid
    }
}