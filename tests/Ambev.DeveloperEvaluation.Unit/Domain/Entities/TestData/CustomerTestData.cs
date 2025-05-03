using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
///     Provides factory methods for generating test data for the Customer entity.
/// </summary>
public static class CustomerTestData
{
    private static readonly Faker Faker = new();

    /// <summary>
    ///     Generates a valid Customer entity with randomized data.
    /// </summary>
    public static Customer GenerateValidCustomer()
    {
        return new Customer(
            Faker.Name.FullName(),
            Faker.Internet.Email(),
            GenerateValidPhone());
    }

    public static string GenerateValidPhone()
    {
        return $"+55{Faker.Random.Number(11, 99)}{Faker.Random.Number(100000000, 999999999)}";
    }

    public static string GenerateInvalidPhone()
    {
        return "123"; // Invalid: too short
    }

    public static string GenerateInvalidEmail()
    {
        return "invalid_email"; // Invalid: missing @
    }

    public static string GenerateInvalidName()
    {
        return "";
    }
}