namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
///     Represents a request to retrieve a sale transaction in the system.
/// </summary>
public class GetSaleRequest
{
    /// <summary>
    ///     Gets or sets the unique identifier of the sale to be retrieved.
    /// </summary>
    public Guid Id { get; set; }
}