using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a branch where sales transactions take place.
/// This entity follows domain-driven design principles.
/// </summary>
public class Branch : BaseEntity
{
    /// <summary>
    /// Gets the branch name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets the branch location address.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets the contact number for the branch.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets the date when the branch was created in the system.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date of the last update to branch details.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Branch class.
    /// </summary>
    public Branch()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Initializes a new instance of the Branch class.
    /// </summary>
    public Branch(string name, string address, string phone)
    {
        Name = name;
        Address = address;
        Phone = phone;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the branch contact details.
    /// </summary>
    public void UpdateBranchInfo(string address, string phone)
    {
        Address = address;
        Phone = phone;
        UpdatedAt = DateTime.UtcNow;
    }
}
