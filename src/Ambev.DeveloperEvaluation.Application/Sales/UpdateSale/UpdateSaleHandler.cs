using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IMapper _mapper;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public UpdateSaleHandler(ISaleRepository saleRepository, ISaleItemRepository saleItemRepository, IMapper mapper, IDomainEventDispatcher domainEventDispatcher)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _domainEventDispatcher = domainEventDispatcher;
        _saleItemRepository = saleItemRepository;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingSale == null)
            throw new InvalidOperationException("Sale not found.");

        // Apply updates
        bool itemsUpdated = existingSale.Items!.SequenceEqual(command.Items, new SaleItemComparer());
        
        existingSale.SaleNumber = command.SaleNumber;
        existingSale.SaleDate = command.SaleDate;
        existingSale.CustomerId = command.CustomerId;
        existingSale.BranchId = command.BranchId;
        existingSale.Items = command.Items;
        if (command.IsCancelled && !existingSale.IsCancelled)
        {
            existingSale.CancelSale();
        }
        existingSale.UpdatedAt = DateTime.UtcNow;

        if (itemsUpdated)
        {
            await _saleItemRepository.DeleteBySaleAsync(existingSale.Id);
            foreach (var item in existingSale.Items)
            {
                await _saleItemRepository.AddAsync(item);
            }
        }

        var updated = await _saleRepository.UpdateAsync(existingSale, cancellationToken);
        
        // Mark event as created.
        updated.UpdateSale();
        updated.UpdateSale();
        await _domainEventDispatcher.DispatchAsync(updated.DomainEvents, cancellationToken);
        updated.ClearDomainEvents();

        return _mapper.Map<UpdateSaleResult>(updated);
    }
}