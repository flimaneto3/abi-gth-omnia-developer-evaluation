namespace Ambev.DeveloperEvaluation.Domain.Events;

public record SaleCreated(Guid SaleId) : DomainEvent;

public record SaleModified(Guid SaleId) : DomainEvent;

public record SaleUpdated(Guid SaleId) : DomainEvent;

public record SaleCancelled(Guid SaleId) : DomainEvent;

public record ItemCancelled(Guid SaleId, Guid ProductId) : DomainEvent;
