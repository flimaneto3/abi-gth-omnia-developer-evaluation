using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
///     Command for creating a new sale transaction.
/// </summary>
/// <remarks>
///     This command captures the required data for recording a sale, including
///     sale number, customer ID, branch ID, sale items, and total amount.
///     It implements <see cref="IRequest{TResponse}" /> to initiate the request
///     that returns a <see cref="CreateSaleResult" />.
///     The data provided in this command is validated using the
///     <see cref="CreateSaleCommandValidator" /> which extends
///     <see cref="AbstractValidator{T}" /> to ensure that the fields follow business rules.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    ///     Gets or sets the sale number used for tracking.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    ///     Gets or sets the customer ID associated with the sale.
    ///     External Identity Pattern is used to reference customers from another domain.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    ///     Gets or sets the branch ID where the sale was made.
    ///     External Identity Pattern is used to reference branches from another domain.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    ///     Gets or sets the list of items in the sale.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();

    /// <summary>
    ///     Indicates whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    ///     Validates the sale command using business rules.
    /// </summary>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}