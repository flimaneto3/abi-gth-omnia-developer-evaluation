using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Profile;

/// <summary>
///     Unit tests for the GetSaleProfile AutoMapper profile.
/// </summary>
public class GetSaleProfileTests
{
    private readonly IMapper _mapper;

    public GetSaleProfileTests()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<GetSaleProfile>(); });

        config.AssertConfigurationIsValid(); // Validates that all mappings are complete
        _mapper = config.CreateMapper();
    }

    [Fact(DisplayName = "Should map Sale to GetSaleResult correctly")]
    public void Sale_Should_Map_To_GetSaleResult()
    {
        // Arrange
        var faker = new Faker();
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = faker.Random.AlphaNumeric(10),
            SaleDate = DateTime.UtcNow,
            Customer = new Customer("Test Customer", "test@email.com", "+5511988888888"),
            Branch = new Branch("Test Branch", "Rua Exemplo, 123", "+551134567890"),
            Items = new List<SaleItem>
            {
                new(Guid.NewGuid(), 2, 100, 20),
                new(Guid.NewGuid(), 1, 200, 0)
            }
        };

        // Act
        var result = _mapper.Map<GetSaleResult>(sale);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        result.SaleNumber.Should().Be(sale.SaleNumber);
        result.SaleDate.Should().BeCloseTo(sale.SaleDate, TimeSpan.FromSeconds(1));
        result.Customer.Should().BeEquivalentTo(sale.Customer);
        result.Branch.Should().BeEquivalentTo(sale.Branch);
        result.Items.Should().BeEquivalentTo(sale.Items);
        result.TotalAmount.Should().Be(sale.TotalAmount);
        result.IsCancelled.Should().Be(sale.IsCancelled);
    }
}