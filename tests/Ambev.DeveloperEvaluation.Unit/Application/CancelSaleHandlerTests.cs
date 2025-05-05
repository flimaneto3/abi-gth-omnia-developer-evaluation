using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CancelSaleHandlerTests
{
    private readonly CancelSaleHandler _handler;
    private readonly ISaleRepository _saleRepository;
    private readonly IDomainEventDispatcher _eventDispatcher;

    public CancelSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _eventDispatcher = Substitute.For<IDomainEventDispatcher>();
        _handler = new CancelSaleHandler(_saleRepository, _eventDispatcher);
    }

    [Fact(DisplayName = "Given existing sale When cancelling Then returns success response")]
    public async Task Handle_ExistingSale_CancelsSuccessfully()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId };
        sale.CancelSale(); // Garante que IsCancelled está true e evento existe

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(sale);

        _saleRepository.UpdateAsync(sale, Arg.Any<CancellationToken>())
            .Returns(sale);

        var command = new CancelSaleCommand(saleId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Sale canceled successfully.");
    }

    [Fact(DisplayName = "Given existing sale When cancellation fails Then returns failure response")]
    public async Task Handle_ExistingSale_CancellationFails_ReturnsFailure()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(sale);

        _saleRepository.UpdateAsync(sale, Arg.Any<CancellationToken>())
            .Returns(new Sale { Id = saleId });

        var command = new CancelSaleCommand(saleId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Failed to cancel sale.");
    }

    [Fact(DisplayName = "Given non-existing sale When cancelling Then throws exception")]
    public async Task Handle_SaleDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns((Sale)null!);

        var command = new CancelSaleCommand(saleId);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Sale with ID {saleId} does not exist.");
    }
}