using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
///     Contains unit tests for the <see cref="DeleteSaleHandler" /> class.
/// </summary>
public class DeleteSaleHandlerTests
{
    private readonly DeleteSaleHandler _handler;
    private readonly ISaleRepository _saleRepository;

    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new DeleteSaleHandler(_saleRepository);
    }

    [Fact(DisplayName = "Given existing sale When deleting Then returns success response")]
    public async Task Handle_ExistingSale_DeletesSuccessfully()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(sale);

        _saleRepository.DeleteAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(true);

        var command = new DeleteSaleCommand(saleId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Sale deleted successfully.");
        await _saleRepository.Received(1).DeleteAsync(saleId, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given existing sale When deletion fails Then returns failure response")]
    public async Task Handle_ExistingSale_DeleteFails_ReturnsFailure()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(sale);

        _saleRepository.DeleteAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(false);

        var command = new DeleteSaleCommand(saleId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Failed to delete sale.");
    }

    [Fact(DisplayName = "Given non-existing sale When deleting Then throws exception")]
    public async Task Handle_SaleDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
            .Returns((Sale)null!);

        var command = new DeleteSaleCommand(saleId);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Sale with ID {saleId} does not exist.");
    }
}