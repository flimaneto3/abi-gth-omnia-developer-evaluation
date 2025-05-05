namespace Ambev.DeveloperEvaluation.Domain.Events;

public abstract record DomainEvent : IDomainEvent
{
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
}

public interface IDomainEvent
{
    DateTime OccurredAt { get; }
}