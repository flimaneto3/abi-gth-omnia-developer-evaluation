using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.ORM.Data;

public class SeedData
{
    public void Development(DefaultContext db)
    {
        if(!db.Branchs.Any(item => item.Id == Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")))
        {
            db.Branchs.AddRange(
                new Branch
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), 
                    Name = "Main Branch", 
                    Address = "123 Business St, São Paulo", 
                    Phone = "+55 11 99999-8888", 
                    CreatedAt = DateTime.UtcNow
                }
            );
        }

        if(!db.Customers.Any(item => item.Id == Guid.Parse("4eb85f64-6789-1234-b3fc-2c963f66afa6")))
        {
            db.Customers.AddRange(
            new Customer
                {
                    Id = Guid.Parse("4eb85f64-6789-1234-b3fc-2c963f66afa6"), 
                    Name = "Fernando Souza", 
                    Email = "fernando@example.com", 
                    Phone = "+55 11 77777-6666", 
                    CreatedAt = DateTime.UtcNow
                }
            );
        }

        if(!db.Products.Any(item => item.Id == Guid.Parse("5fb85f64-1234-5678-b3fc-2c963f66afa6")))
        {
            db.Products.AddRange(
            new Product
                {
                    Id = Guid.Parse("5fb85f64-1234-5678-b3fc-2c963f66afa6"), 
                    Name = "Laptop Pro", 
                    Description = "High-end business laptop", 
                    Price = 4999.99m, 
                    StockQuantity = 50, 
                    CreatedAt = DateTime.UtcNow
                }
            );
        }

        if(!db.Sales.Any(item => item.Id == Guid.Parse("6db85f64-9876-5432-b3fc-2c963f66afa6")))
        {
                    
            db.Sales.AddRange(
                new Sale
                {
                    Id = Guid.Parse("6db85f64-9876-5432-b3fc-2c963f66afa6"), 
                    SaleNumber = "SL-001", 
                    SaleDate = DateTime.UtcNow, 
                    CustomerId = Guid.Parse("4eb85f64-6789-1234-b3fc-2c963f66afa6"), 
                    BranchId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), 
                }
            );
        }

        if(!db.SaleItems.Any(item => item.Id == Guid.Parse("7fb85f64-5555-4444-b3fc-2c963f66afa6")))
        {
            db.SaleItems.AddRange(
                new SaleItem
                {
                    Id = Guid.Parse("7fb85f64-5555-4444-b3fc-2c963f66afa6"), 
                    ProductId = Guid.Parse("5fb85f64-1234-5678-b3fc-2c963f66afa6"), 
                    Quantity = 1, 
                    UnitPrice = 4999.99m, 
                    Discount = 0.00m
                }
            );
        }

        if(!db.Users.Any(item => item.Id == Guid.Parse("8db85f64-2222-1111-b3fc-2c963f66afa6")))
        {
            db.Users.AddRange(
                new User
                {
                    Id = Guid.Parse("8db85f64-2222-1111-b3fc-2c963f66afa6"), 
                    Username = "admin_user", 
                    Password = "securepasswordhash", 
                    Phone = "+55 11 55555-4444", 
                    Email = "admin@example.com", 
                    Status = UserStatus.Active, 
                    Role = UserRole.Admin, 
                    CreatedAt = DateTime.UtcNow
                }
            );
        }

        db.SaveChanges();
    }    
}