using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        RuleFor(s => s.Id).NotEqual(Guid.Empty).WithMessage("Sale ID is required.");
        RuleFor(s => s.SaleNumber).NotEmpty().Length(3, 50);
        RuleFor(s => s.SaleDate).LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(s => s.CustomerId).NotEqual(Guid.Empty);
        RuleFor(s => s.BranchId).NotEqual(Guid.Empty);
        RuleFor(s => s.Items)
            .NotEmpty().WithMessage("Sale must have at least one item.");

        RuleForEach(s => s.Items)
            .SetValidator(new SaleItemValidator());
    }
}

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(i => i.ProductId).NotEqual(Guid.Empty);
        RuleFor(i => i.Quantity).GreaterThan(0);
    }
}