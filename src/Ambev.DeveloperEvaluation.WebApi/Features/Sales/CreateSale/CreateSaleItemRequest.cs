using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents an item in a sale request.
/// </summary>
public class SaleItemRequest
{
    /// <summary>
    /// The unique identifier of the product being sold.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product being purchased.
    /// </summary>
    public int Quantity { get; set; }
}