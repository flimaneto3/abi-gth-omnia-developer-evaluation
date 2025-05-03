using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Profile;

/// <summary>
///     Unit tests for the CreateSaleProfile AutoMapper profile.
/// </summary>
public class CreateSaleProfileTests
{
    private readonly IMapper _mapper;

    public CreateSaleProfileTests()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<CreateSaleProfile>(); });

        config.AssertConfigurationIsValid(); // Ensure mappings are valid
        _mapper = config.CreateMapper();
    }

    [Fact(DisplayName = "Should map CreateSaleCommand to Sale correctly")]
    public void CreateSaleCommand_Should_Map_To_Sale()
    {
        // Arrange
        var faker = new Faker();
        var command = new CreateSaleCommand
        {
            SaleNumber = faker.Random.AlphaNumeric(10),
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<SaleItem>
            {
                new(Guid.NewGuid(), 2, 100, 10)
            },
            IsCancelled = false
        };

        // Act
        var sale = _mapper.Map<Sale>(command);

        // Assert
        sale.Should().NotBeNull();
        sale.SaleNumber.Should().Be(command.SaleNumber);
        sale.CustomerId.Should().Be(command.CustomerId);
        sale.BranchId.Should().Be(command.BranchId);
        sale.Items.Should().BeEquivalentTo(command.Items);
        sale.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        sale.Id.Should().Be(Guid.Empty); // Id is ignored
    }

    [Fact(DisplayName = "Should map Sale to CreateSaleResult correctly")]
    public void Sale_Should_Map_To_CreateSaleResult()
    {
        // Arrange
        var faker = new Faker();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = "SALE123",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Customer = new Customer("Test Customer", "email@example.com", "+5511999999999"),
            Branch = new Branch("Branch A", "Av. Brasil, 123", "+551132345678"),
            Items = new List<SaleItem>
            {
                new(Guid.NewGuid(), 1, 150, 0)
            },
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = _mapper.Map<CreateSaleResult>(sale);

        // Assert
        result.Should().NotBeNull();
        result.SaleId.Should().Be(sale.Id);
        result.SaleNumber.Should().Be(sale.SaleNumber);
        result.Customer.Should().BeEquivalentTo(sale.Customer);
        result.Branch.Should().BeEquivalentTo(sale.Branch);
        result.Items.Should().BeEquivalentTo(sale.Items);
        result.TotalAmount.Should().Be(sale.TotalAmount);
        result.IsCancelled.Should().Be(sale.IsCancelled);
    }
}