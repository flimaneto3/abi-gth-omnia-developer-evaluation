using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
///     Validator for CancelSaleRequest that defines validation rules for sale deletion.
/// </summary>
public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
{
    /// <summary>
    ///     Initializes a new instance of the CancelSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    ///     Validation rules include:
    ///     - Id: Must be a valid non-empty GUID.
    /// </remarks>
    public CancelSaleRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("Sale ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid Sale ID.");
    }
}