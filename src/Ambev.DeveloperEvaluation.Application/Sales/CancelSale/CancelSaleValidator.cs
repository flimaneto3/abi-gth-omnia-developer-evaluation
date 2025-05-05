using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
///     Validator for CancelSaleCommand that defines validation rules for sale deletion.
/// </summary>
public class CancelSaleValidator : AbstractValidator<CancelSaleCommand>
{
    /// <summary>
    ///     Initializes a new instance of the CancelSaleValidator with defined validation rules.
    /// </summary>
    public CancelSaleValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Sale ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid Sale ID.");
    }
}