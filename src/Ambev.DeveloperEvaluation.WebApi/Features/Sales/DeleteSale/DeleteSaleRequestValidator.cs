using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
///     Validator for DeleteSaleRequest that defines validation rules for sale deletion.
/// </summary>
public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
{
    /// <summary>
    ///     Initializes a new instance of the DeleteSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    ///     Validation rules include:
    ///     - Id: Must be a valid non-empty GUID.
    /// </remarks>
    public DeleteSaleRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("Sale ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid Sale ID.");
    }
}