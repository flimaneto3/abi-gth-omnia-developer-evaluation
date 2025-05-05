namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
///     Represents a request to cancel a sale transaction in the system.
/// </summary>
public class CancelSaleRequest
{
    /// <summary>
    ///     Gets or sets the unique identifier of the sale to be canceled.
    /// </summary>
    public Guid Id { get; set; }
}