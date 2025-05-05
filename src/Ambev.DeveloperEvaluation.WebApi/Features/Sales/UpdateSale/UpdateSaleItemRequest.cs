namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents an item in an update sale request.
/// </summary>
public class UpdateSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the product being updated.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The new quantity of the product.
    /// </summary>
    public int Quantity { get; set; }
}