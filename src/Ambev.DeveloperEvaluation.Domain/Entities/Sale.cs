using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
///     Represents a sale transaction in the system.
///     Follows domain-driven design principles with validation and business rules.
/// </summary>
public class Sale : BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private void AddEvent(IDomainEvent @event) => _domainEvents.Add(@event);

    /// <summary>
    ///     Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
        IsCancelled = false;
    }

    /// <summary>
    ///     Initializes a new sale transaction.
    /// </summary>
    public Sale(string saleNumber, Guid customerId, Guid branchId, List<SaleItem> items, Customer customer)
    {
        SaleNumber = saleNumber;
        SaleDate = DateTime.UtcNow;
        CustomerId = customerId;
        BranchId = branchId;
        Items = items;
        CreatedAt = DateTime.UtcNow;
        Customer = customer;
        IsCancelled = false;
    }

    /// <summary>
    ///     Gets the sale number, used as a reference for tracking.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    ///     Gets the date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    ///     Gets the customer associated with the sale.
    ///     External Identity Pattern is used to reference customers from another domain.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    ///     Gets the branch where the sale was made.
    ///     External Identity Pattern is used to reference branches from another domain.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    ///     Gets the list of items in this sale transaction.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();

    /// <summary>
    ///     Gets the customer associated with the sale.
    ///     Stores full customer details as an object.
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    ///     Gets the branch where the sale was made.
    ///     Stores full branch details as an object.
    /// </summary>
    public Branch Branch { get; set; }

    /// <summary>
    ///     Gets the total amount for the sale.
    ///     Automatically calculated from the items.
    /// </summary>
    public decimal TotalAmount => Items.Sum(i => i.TotalItemPrice);

    /// <summary>
    ///     Indicates whether the sale was canceled.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    ///     Gets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Gets the date and time of the last update to the sale.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    ///     Cancels the sale, marking it as invalid.
    /// </summary>
    public void CancelSale()
    {
        IsCancelled = true;
        UpdatedAt = DateTime.UtcNow;
        AddEvent(new SaleCancelled(Id));

        foreach (var item in Items)
        {
            AddEvent(new ItemCancelled(Id, item.ProductId));
        }
    }
    
    /// <summary>
    ///     Cancels the sale, marking it as invalid.
    /// </summary>
    public void CancelItem(Guid id, Guid productId)
    {
        AddEvent(new ItemCancelled(id, productId));
    }

    /// <summary>
    ///     Create a sale, create an event.
    /// </summary>
    public void CreateSale()
    {
        AddEvent(new SaleCreated(Id));
    }
    
    /// <summary>
    ///     Update a sale, create an event.
    /// </summary>
    public void UpdateSale()
    {
        AddEvent(new SaleUpdated(Id));
    }
    
    /// <summary>
    ///     Modify a sale, modify event.
    /// </summary>
    public void ModifySale()
    {
        AddEvent(new SaleModified(Id));
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}