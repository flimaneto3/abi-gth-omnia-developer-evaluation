using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
///     Handler for processing DeleteSaleCommand requests.
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    ///     Initializes a new instance of DeleteSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    public DeleteSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    ///     Handles the DeleteSaleCommand request.
    /// </summary>
    /// <param name="command">The delete sale command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result of the sale deletion process.</returns>
    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException($"Sale with ID {command.Id} does not exist.");

        var deleted = await _saleRepository.DeleteAsync(command.Id, cancellationToken);
        return new DeleteSaleResponse
        {
            Success = deleted,
            Message = deleted ? "Sale deleted successfully." : "Failed to delete sale."
        };
    }
}