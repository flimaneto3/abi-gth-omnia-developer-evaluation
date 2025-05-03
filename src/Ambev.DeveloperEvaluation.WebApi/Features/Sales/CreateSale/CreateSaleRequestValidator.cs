using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
///     Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    ///     Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    ///     Validation rules include:
    ///     - SaleNumber: Required, length between 3 and 50 characters
    ///     - SaleDate: Cannot be in the future
    ///     - Customer: Required
    ///     - Branch: Required
    ///     - Items: Must contain at least one item
    ///     - TotalAmount: Must be greater than zero
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.SaleNumber).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.SaleDate).LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(sale => sale.CustomerId).NotNull().NotEqual(Guid.Empty)
            .WithMessage("Customer information is required.");
        RuleFor(sale => sale.BranchId).NotNull().NotEqual(Guid.Empty).NotEqual(Guid.Empty)
            .WithMessage("Branch information is required.");
        RuleFor(sale => sale.Items).NotEmpty().WithMessage("Sale must have at least one item.");
        RuleForEach(sale => sale.Items).SetValidator(new CreateSaleItemRequestValidator());
    }
}