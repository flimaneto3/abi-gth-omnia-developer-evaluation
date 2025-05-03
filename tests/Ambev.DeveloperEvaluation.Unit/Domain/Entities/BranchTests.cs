using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
///     Contains unit tests for the Branch entity.
///     Tests creation, updates, and data validation scenarios.
/// </summary>
public class BranchTests
{
    [Fact(DisplayName = "Should initialize CreatedAt on instantiation")]
    public void Given_NewBranch_When_Created_Then_CreatedAtShouldBeSet()
    {
        // Act
        var branch = BranchTestData.GenerateValidBranch();

        // Assert
        Assert.True(branch.CreatedAt <= DateTime.UtcNow);
    }

    [Fact(DisplayName = "Should update address and phone and set UpdatedAt")]
    public void Given_ValidBranch_When_UpdateBranchInfo_Then_AddressPhoneAndUpdatedAtShouldBeSet()
    {
        // Arrange
        var branch = BranchTestData.GenerateValidBranch();
        var newAddress = "New Address, 123";
        var newPhone = "+5511987654321";

        // Act
        branch.UpdateBranchInfo(newAddress, newPhone);

        // Assert
        Assert.Equal(newAddress, branch.Address);
        Assert.Equal(newPhone, branch.Phone);
        Assert.NotNull(branch.UpdatedAt);
        Assert.True(branch.UpdatedAt <= DateTime.UtcNow);
    }

    [Fact(DisplayName = "Should assign valid name, address and phone")]
    public void Given_ValidBranch_When_Created_Then_PropertiesShouldBeCorrect()
    {
        // Arrange
        var branch = BranchTestData.GenerateValidBranch();

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(branch.Name));
        Assert.False(string.IsNullOrWhiteSpace(branch.Address));
        Assert.StartsWith("+55", branch.Phone);
    }
}