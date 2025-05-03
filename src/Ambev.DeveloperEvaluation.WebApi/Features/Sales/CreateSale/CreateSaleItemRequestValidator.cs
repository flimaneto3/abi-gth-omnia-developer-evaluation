using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
///     Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<SaleItemRequest>
{
    /// <summary>
    ///     Initializes a new instance of the CreateSaleItemRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    ///     Validation rules include:
    ///     - Quantity: Required, length greater than 0
    ///     - ProductId: Required
    /// </remarks>
    public CreateSaleItemRequestValidator()
    {
        RuleFor(saleItem => saleItem.Quantity).GreaterThan(0).WithMessage("Sale Item must have at least one item.");
        RuleFor(saleItem => saleItem.ProductId).NotNull().NotEqual(Guid.Empty)
            .WithMessage("Sale Item must have a product id.");
    }
}