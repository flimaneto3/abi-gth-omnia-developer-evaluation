using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
///     Response model for GetSale operation.
/// </summary>
public class GetSaleResult
{
    /// <summary>
    ///     The unique identifier of the retrieved sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     The sale number used for tracking.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    ///     The date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    ///     The customer associated with the sale.
    /// </summary>
    public Customer Customer { get; set; } = new();

    /// <summary>
    ///     The branch where the sale occurred.
    /// </summary>
    public Branch Branch { get; set; } = new();

    /// <summary>
    ///     The list of items included in the sale.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();

    /// <summary>
    ///     The total amount of the sale transaction.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    ///     Indicates whether the sale was cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}