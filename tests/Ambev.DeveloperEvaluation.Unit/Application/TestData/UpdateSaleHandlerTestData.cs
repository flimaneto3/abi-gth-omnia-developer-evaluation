using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class UpdateSaleHandlerTestData
{
    private static readonly Faker Faker = new();

    public static UpdateSaleCommand GenerateValidCommand()
    {
        return new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            SaleNumber = Faker.Random.AlphaNumeric(8),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow.AddMinutes(-5),
            Items = new List<SaleItem>
            {
                new(Guid.NewGuid(), 3, 100, 0)
            },
            IsCancelled = false
        };
    }

    public static UpdateSaleCommand GenerateValidCommandWithItem(int quantity, decimal unitPrice)
    {
        return new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            SaleNumber = Faker.Random.AlphaNumeric(8),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            Items = new List<SaleItem>
            {
                new(Guid.NewGuid(), quantity, unitPrice, 0)
            },
            IsCancelled = false
        };
    }
}