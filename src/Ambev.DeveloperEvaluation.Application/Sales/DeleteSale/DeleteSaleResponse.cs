namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
///     Response model for DeleteSale operation.
/// </summary>
public class DeleteSaleResponse
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