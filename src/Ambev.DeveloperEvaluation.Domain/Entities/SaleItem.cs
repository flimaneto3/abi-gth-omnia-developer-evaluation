using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
///     Represents an item in a sale transaction.
///     Includes product, quantity, pricing, and discount details.
/// </summary>
public class SaleItem : BaseEntity
{
    public SaleItem()
    {
    }

    /// <summary>
    ///     Initializes a new sale item.
    /// </summary>
    public SaleItem(Guid productId, int quantity, decimal unitPrice, decimal discount)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

    /// <summary>
    ///     Gets the external reference to the product being sold.
    ///     External Identity Pattern is used to reference the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    ///     Gets the quantity of the product purchased in the sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    ///     Gets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    ///     Gets the discount applied to the product.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    ///     Gets the total price for this item after applying the discount.
    /// </summary>
    public decimal TotalItemPrice => UnitPrice * Quantity - Discount;

    /// <summary>
    ///     Gets product related to the item.
    /// </summary>
    public Product Product { get; set; }
    
    public Guid SaleId { get; set; }
}

public class SaleItemComparer : IEqualityComparer<SaleItem>
{
    public bool Equals(SaleItem? x, SaleItem? y)
    {
        if (x == null || y == null)
            return false;

        return x.ProductId == y.ProductId &&
               x.Quantity == y.Quantity &&
               x.UnitPrice == y.UnitPrice &&
               x.Discount == y.Discount &&
               x.SaleId == y.SaleId;
    }

    public int GetHashCode(SaleItem obj)
    {
        return HashCode.Combine(obj.ProductId, obj.Quantity, obj.UnitPrice, obj.Discount, obj.SaleId);
    }
}