using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.SalesTests;

public class CreateSaleRequestTests
{
    private readonly CreateSaleRequestValidator _validator;

    public CreateSaleRequestTests()
    {
        _validator = new CreateSaleRequestValidator();
    }

    [Fact]
    public void Valid_CreateSaleRequest_Should_Pass_Validation()
    {
        // Arrange
        var request = new CreateSaleRequest
        {
            SaleNumber = "SL-123",
            SaleDate = DateTime.Now.AddMinutes(-20),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<SaleItemRequest>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 2 }
            },
            IsCancelled = false
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void CreateSaleRequest_With_Empty_SaleNumber_Should_Fail()
    {
        var request = new CreateSaleRequest { SaleNumber = "" };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(s => s.SaleNumber);
    }

    [Fact]
    public void CreateSaleRequest_With_Future_SaleDate_Should_Fail()
    {
        var request = new CreateSaleRequest { SaleDate = DateTime.UtcNow.AddDays(1) };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(s => s.SaleDate);
    }

    [Fact]
    public void CreateSaleRequest_Without_Items_Should_Fail()
    {
        var request = new CreateSaleRequest { Items = new List<SaleItemRequest>() };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(s => s.Items);
    }

    [Fact]
    public void CreateSaleRequest_With_Short_SaleNumber_Should_Fail()
    {
        var request = new CreateSaleRequest { SaleNumber = "AB" }; // Apenas 2 caracteres

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(s => s.SaleNumber);
    }

    [Fact]
    public void CreateSaleRequest_With_Empty_CustomerId_Should_Fail()
    {
        var request = new CreateSaleRequest
        {
            SaleNumber = "12345",
            CustomerId = Guid.Empty,
            Items =
            [
                new SaleItemRequest
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 10
                }
            ],
            BranchId = Guid.NewGuid()
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(s => s.CustomerId);
    }

    [Fact]
    public void CreateSaleRequest_With_Empty_BranchId_Should_Fail()
    {
        var request = new CreateSaleRequest
        {
            SaleNumber = "12345",
            Items = [new SaleItemRequest()],
            BranchId = Guid.Empty
        };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(s => s.BranchId);
    }

    [Fact]
    public void CreateSaleRequest_With_Zero_Quantity_Should_Fail()
    {
        var request = new CreateSaleRequest
        {
            SaleNumber = "12345",
            BranchId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Items = new List<SaleItemRequest> { new() { ProductId = Guid.NewGuid(), Quantity = 0 } }
        };

        var result = _validator.TestValidate(request);

        Assert.Equal("Sale Item must have at least one item.", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public void CreateSaleRequest_With_Empty_Product_Id_Should_Fail()
    {
        var request = new CreateSaleRequest
        {
            SaleNumber = "12345",
            BranchId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Items = new List<SaleItemRequest> { new() { ProductId = Guid.Empty, Quantity = 2 } }
        };

        var result = _validator.TestValidate(request);

        Assert.Equal("Sale Item must have a product id.", result.Errors.First().ErrorMessage);
    }
}