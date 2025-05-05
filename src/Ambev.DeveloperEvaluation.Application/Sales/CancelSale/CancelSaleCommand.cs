using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
///     Command for deleting a sale transaction.
/// </summary>
public class CancelSaleCommand(Guid id) : IRequest<CancelSaleResponse>
{
    /// <summary>
    ///     Gets or sets the unique identifier of the sale to cancel.
    /// </summary>
    public Guid Id { get; set; } = id;
}