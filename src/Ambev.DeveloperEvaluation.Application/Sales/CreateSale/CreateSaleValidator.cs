using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
///     Validator for the CreateSaleCommand.
///     Ensures all required fields are populated and follow business rules.
/// </summary>
public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(s => s.SaleNumber)
            .NotEmpty().WithMessage("Sale number is required.")
            .MaximumLength(50).WithMessage("Sale number cannot exceed 50 characters.");

        RuleFor(s => s.SaleDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(s => s.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(s => s.BranchId)
            .NotEmpty().WithMessage("Branch ID is required.");

        RuleFor(s => s.Items)
            .NotEmpty().WithMessage("Sale must have at least one item.");

        RuleForEach(s => s.Items)
            .SetValidator(new SaleItemValidator());
    }
}

/// <summary>
///     Validator for SaleItem to apply specific business rules.
/// </summary>
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(i => i.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

        RuleFor(i => i.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

        RuleFor(i => i.Discount)
            .Must((item, discount) => ValidateDiscount(item.Quantity, item.UnitPrice, discount))
            .WithMessage("Invalid discount applied based on quantity.");
    }

    private bool ValidateDiscount(int quantity, decimal unitPrice, decimal discount)
    {
        if (quantity < 4 && discount > 0) return false; // ❌ No discount for less than 4 items.
        if (quantity >= 4 && quantity < 10 && discount != quantity * unitPrice * 0.10m) return false; // ✅ 10% discount.
        if (quantity >= 10 && quantity <= 20 && discount != quantity * unitPrice * 0.20m)
            return false; // ✅ 20% discount.
        return true;
    }
}