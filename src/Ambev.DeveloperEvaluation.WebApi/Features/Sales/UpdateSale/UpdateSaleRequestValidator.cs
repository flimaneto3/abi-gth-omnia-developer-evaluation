using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleRequest.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(sale => sale.Id)
            .NotEqual(Guid.Empty).WithMessage("Sale ID is required.");

        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().Length(3, 50);

        RuleFor(sale => sale.SaleDate)
            .LessThanOrEqualTo(DateTime.UtcNow);

        RuleFor(sale => sale.CustomerId)
            .NotEqual(Guid.Empty).WithMessage("Customer ID is required.");

        RuleFor(sale => sale.BranchId)
            .NotEqual(Guid.Empty).WithMessage("Branch ID is required.");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("Sale must contain at least one item.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new UpdateSaleItemRequestValidator());
    }
}