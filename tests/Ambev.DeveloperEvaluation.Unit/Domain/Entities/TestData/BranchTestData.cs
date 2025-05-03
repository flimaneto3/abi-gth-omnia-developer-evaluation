using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
///     Provides factory methods for generating test data for the Branch entity.
/// </summary>
public static class BranchTestData
{
    private static readonly Faker Faker = new();

    /// <summary>
    ///     Generates a valid Branch entity with random but valid data.
    /// </summary>
    public static Branch GenerateValidBranch()
    {
        return new Branch(
            Faker.Company.CompanyName(),
            Faker.Address.FullAddress(),
            GenerateValidPhone());
    }

    /// <summary>
    ///     Generates an invalid phone number (e.g., too short or malformed).
    /// </summary>
    public static string GenerateInvalidPhone()
    {
        return Faker.Random.String2(5); // Invalid, too short
    }

    /// <summary>
    ///     Generates a valid phone number in Brazilian format.
    /// </summary>
    public static string GenerateValidPhone()
    {
        return $"+55{Faker.Random.Number(11, 99)}{Faker.Random.Number(100000000, 999999999)}";
    }

    /// <summary>
    ///     Generates an invalid address (e.g., empty string).
    /// </summary>
    public static string GenerateInvalidAddress()
    {
        return string.Empty;
    }

    /// <summary>
    ///     Generates an invalid name (e.g., too short or empty).
    /// </summary>
    public static string GenerateInvalidName()
    {
        return " ";
    }
}