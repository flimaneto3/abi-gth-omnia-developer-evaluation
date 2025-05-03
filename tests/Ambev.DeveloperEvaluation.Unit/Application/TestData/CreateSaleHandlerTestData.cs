using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker Faker = new();

    public static CreateSaleCommand GenerateValidCommand()
    {
        return new CreateSaleCommand
        {
            SaleNumber = Faker.Random.AlphaNumeric(8),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<SaleItem>
            {
                new(Guid.NewGuid(), 3, 100, 0)
            }
        };
    }

    public static CreateSaleCommand GenerateValidCommandWithItem(int quantity, decimal unitPrice)
    {
        return new CreateSaleCommand
        {
            SaleNumber = Faker.Random.AlphaNumeric(8),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<SaleItem>
            {
                new(Guid.NewGuid(), quantity, unitPrice, 0)
            }
        };
    }
}