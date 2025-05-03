using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
///     Represents a customer in the system.
///     This entity follows domain-driven design principles.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    ///     Initializes a new instance of the Customer class.
    /// </summary>
    public Customer()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     Initializes a new instance of the Customer class.
    /// </summary>
    public Customer(string name, string email, string phone)
    {
        Name = name;
        Email = email;
        Phone = phone;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     Gets the customer's full name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets the customer's email address.
    ///     Used for communication and identification.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     Gets the customer's phone number.
    ///     Used for contact and support purposes.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    ///     Gets the date when the customer registered.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Gets the date of the last update made to customer details.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    ///     Updates the customer's contact information.
    /// </summary>
    public void UpdateContactInfo(string email, string phone)
    {
        Email = email;
        Phone = phone;
        UpdatedAt = DateTime.UtcNow;
    }
}