using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
///     Command for deleting a sale transaction.
/// </summary>
public class DeleteSaleCommand(Guid id) : IRequest<DeleteSaleResponse>
{
    /// <summary>
    ///     Gets or sets the unique identifier of the sale to delete.
    /// </summary>
    public Guid Id { get; set; } = id;
}