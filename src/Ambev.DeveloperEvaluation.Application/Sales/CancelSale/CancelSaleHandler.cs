using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
///     Handler for processing CancelSaleCommand requests.
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    /// <summary>
    ///     Initializes a new instance of CancelSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    public CancelSaleHandler(ISaleRepository saleRepository, IDomainEventDispatcher domainEventDispatcher)
    {
        _saleRepository = saleRepository;
        _domainEventDispatcher = domainEventDispatcher;
    }

    /// <summary>
    ///     Handles the CancelSaleCommand request.
    /// </summary>
    /// <param name="command">The cancel sale command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result of the sale deletion process.</returns>
    public async Task<CancelSaleResponse> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException($"Sale with ID {command.Id} does not exist.");

        sale.CancelSale();
        
        var cancelled = await _saleRepository.UpdateAsync(sale, cancellationToken);
        
        // Mark event as created.
        sale.CancelSale();
        await _domainEventDispatcher.DispatchAsync(sale.DomainEvents, cancellationToken);
        sale.ClearDomainEvents();
        
        return new CancelSaleResponse
        {
            Success = cancelled.IsCancelled,
            Message = cancelled.IsCancelled ? "Sale canceled successfully." : "Failed to cancel sale."
        };
    }
}