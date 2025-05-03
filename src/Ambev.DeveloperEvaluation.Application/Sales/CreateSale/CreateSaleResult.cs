using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
///     Represents the result of a sale creation operation.
/// </summary>
/// <remarks>
///     This result object is returned upon successfully processing a sale request.
///     It contains the generated sale ID, sale number, date, customer details, branch,
///     list of items, total amount, and cancellation status.
/// </remarks>
public class CreateSaleResult
{
    public CreateSaleResult()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the CreateSaleResult class.
    /// </summary>
    public CreateSaleResult(Sale sale)
    {
        SaleId = sale.Id;
        SaleNumber = sale.SaleNumber;
        SaleDate = sale.SaleDate;
        Customer = sale.Customer;
        Branch = sale.Branch;
        Items = sale.Items;
        TotalAmount = sale.TotalAmount;
        IsCancelled = sale.IsCancelled;
    }

    public Guid Id { get; set; }

    /// <summary>
    ///     Gets the unique identifier for the sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    ///     Gets the sale number used for tracking.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    ///     Gets the date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    ///     Gets the customer associated with the sale.
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    ///     Gets the branch where the sale occurred.
    /// </summary>
    public Branch Branch { get; set; } = new();

    /// <summary>
    ///     Gets the list of items included in the sale.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();

    /// <summary>
    ///     Gets the total amount of the sale transaction.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    ///     Indicates whether the sale was cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}