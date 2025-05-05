using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleHandlerTests
{
    private readonly CreateSaleHandler _handler;
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _domainEventDispatcher = Substitute.For<IDomainEventDispatcher>();

        _handler = new CreateSaleHandler(_saleRepository, _productRepository, _mapper, _domainEventDispatcher);
    }

    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response and dispatches event")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse_And_DispatchesDomainEvent()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale { Id = Guid.NewGuid() };
        sale.CreateSale(); // Garante que evento está presente

        foreach (var item in command.Items)
        {
            var product = ProductTestData.GenerateValidProduct();
            _productRepository.GetByIdAsync(item.ProductId).Returns(product);
        }

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(sale, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(new CreateSaleResult { SaleId = sale.Id });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.SaleId.Should().Be(sale.Id);
    }

    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateSaleCommand(); // Invalid
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Act & Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Theory(DisplayName = "Given sale items When calculating discount Then applies correct discount")]
    [InlineData(2, 100, 0)]
    [InlineData(5, 100, 50)]
    [InlineData(10, 100, 200)]
    public async Task Handle_ShouldCalculateCorrectDiscount(int quantity, decimal price, decimal expectedDiscount)
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommandWithItem(quantity, price);

        var product = new Product("Product", "Desc", price, 100) { Id = command.Items[0].ProductId };
        _productRepository.GetByIdAsync(product.Id).Returns(product);

        var sale = new Sale { Id = Guid.NewGuid() };
        sale.CreateSale(); // Adiciona evento

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(new CreateSaleResult { SaleId = sale.Id });
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var item = command.Items[0];
        item.Discount.Should().BeApproximately(expectedDiscount, 0.01m);
    }

    [Fact(DisplayName = "Given quantity above 20 When handling sale Then throws invalid operation")]
    public async Task Handle_QuantityAboveLimit_ThrowsInvalidOperation()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommandWithItem(25, 100);
        var product = ProductTestData.GenerateValidProduct();
        _productRepository.GetByIdAsync(command.Items[0].ProductId).Returns(product);

        var act = () => _handler.Handle(command, CancellationToken.None);

        // Act & Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Cannot sell more than 20 identical items.");
    }
}
