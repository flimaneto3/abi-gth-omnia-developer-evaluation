using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IProductRepository productRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request.
    /// </summary>
    /// <param name="command">The CreateSale command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created sale details.</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            item.UnitPrice = product.Price;
            item.Product = product;
            item.Discount = CalculateDiscount(item.Quantity, item.UnitPrice);
        }
        
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }

    /// <summary>
    /// Calculates the discount based on the item quantity.
    /// </summary>
    /// <param name="quantity">The quantity of items.</param>
    /// <param name="unitPrice">The unit price of the product.</param>
    /// <returns>The calculated discount value.</returns>
    private decimal CalculateDiscount(int quantity, decimal unitPrice)
    {
        if (quantity < 4) return 0; // ❌ No discount for less than 4 items
        if (quantity >= 4 && quantity < 10) return quantity * unitPrice * 0.10m; // ✅ 10% discount
        if (quantity >= 10 && quantity <= 20) return quantity * unitPrice * 0.20m; // ✅ 20% discount
        throw new InvalidOperationException("Cannot sell more than 20 identical items.");
    }
}
