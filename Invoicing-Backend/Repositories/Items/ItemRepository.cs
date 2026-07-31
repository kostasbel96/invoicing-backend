using Invoicing_Backend.Data;
using Invoicing_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoicing_Backend.Repositories.Items;

public class ItemRepository : BaseRepository<Item>, IItemRepository
{
    public ItemRepository(InvoicingAppDbContext context) : base(context)
    {
    }

    public async Task<PaginatedResult<Item>> GetPaginatedItemsAsync(int pageNumber, 
        int pageSize, string searchTerm, string sortField, string sortOrder)
    {
        var query = context.Items
            .Where(x => x.IsActive)
            .AsQueryable();
        
        string search = searchTerm.ToUpper();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.Name.ToUpper().Contains(search)
            );
        }
        if (!string.IsNullOrEmpty(sortField))
        {
            switch (sortField.ToLower())
            {
                case "name":
                    query = sortOrder == "ASC"
                        ? query.OrderBy(x => x.Name)
                        : query.OrderByDescending(x => x.Name);
                    break;

                default:
                    query = query.OrderBy(x => x.Id);
                    break;
            }
        }
        else
        {
            query = query.OrderBy(x => x.Id);
        }

        var totalRecords = await query.CountAsync();

        int skip = pageNumber * pageSize;

        var items = await query
            .Include(x => x.VatRate)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Item>
        {
            Data = items,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task AddAsync(Item item) => await dbset.AddAsync(item);

    public Task UpdateAsync(Item item)
    {
        dbset.Attach(item);
        context.Entry(item).State = EntityState.Modified; 
        return Task.CompletedTask;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Item? existingItem = await GetByIdAsync(id);
        if (existingItem is null) return false;
        dbset.Remove(existingItem);
        return true;
    }

    public async Task<bool> NameExistsAsync(string name)
    {
        return await dbset.AnyAsync(x => x.Name == name);
    }

    public async Task<bool> NameExistsForOtherAsync(Guid uuid, string name)
    {
        return await dbset.AnyAsync(x =>
            x.Name == name &&
            x.Uuid != uuid);
    }
}