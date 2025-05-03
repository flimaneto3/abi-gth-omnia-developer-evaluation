using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
///     Contains unit tests for the <see cref="GetSaleHandler" /> class.
/// </summary>
public class GetSaleHandlerTests
{
    private readonly GetSaleHandler _handler;
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;

    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid command When sale exists Then returns sale result")]
    public async Task Handle_ExistingSale_ReturnsMappedResult()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand { Id = saleId };

        var sale = new Sale { Id = saleId };
        var expectedResult = new GetSaleResult { Id = saleId };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(saleId);
    }

    [Fact(DisplayName = "Given valid command When sale does not exist Then returns null")]
    public async Task Handle_SaleNotFound_ReturnsDefault()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new GetSaleCommand { Id = saleId };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact(DisplayName = "Given invalid command When handling Then throws validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Arrange
        var command = new GetSaleCommand(); // Id vazio é inválido
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Act & Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}