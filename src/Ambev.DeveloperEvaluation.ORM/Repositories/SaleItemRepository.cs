using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<SaleItem> GetByIdAsync(Guid id)
    {
        return (await _context.SaleItems
            .Include(s => s.Product)
            .FirstOrDefaultAsync(s => s.Id == id))!;
    }

    public async Task<IEnumerable<SaleItem>> GetAllAsync()
    {
        return await _context.SaleItems.ToListAsync();
    }

    public async Task AddAsync(SaleItem saleItem)
    {
        await _context.SaleItems.AddAsync(saleItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SaleItem saleItem)
    {
        _context.SaleItems.Update(saleItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var saleItem = await _context.SaleItems.FindAsync(id);
        if (saleItem != null)
        {
            _context.SaleItems.Remove(saleItem);
            await _context.SaveChangesAsync();
        }
    }
}