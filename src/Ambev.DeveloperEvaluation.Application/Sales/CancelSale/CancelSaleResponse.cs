namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
///     Response model for CancelSale operation.
/// </summary>
public class CancelSaleResponse
{
    /// <summary>
    ///     Indicates whether the sale deletion was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    ///     Message detailing the result of the operation.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}