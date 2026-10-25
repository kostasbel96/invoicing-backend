using Invoicing_Backend.Data;
using Invoicing_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoicing_Backend.Repositories.Items.Service;

public class ServiceItemRepository : BaseRepository<ServiceItem>, IItemRepository
{
    public ServiceItemRepository(InvoicingAppDbContext context) : base(context)
    {
    }

    public async Task<PaginatedResult<Item>> GetPaginatedItemsAsync(int pageNumber, int pageSize, string searchTerm, string sortField, string sortOrder)
    {
        var query = context.ServiceItems
            .AsNoTracking()
            .Where(x => x.IsActive)
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string search = searchTerm.ToUpper();
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
                
                case "description":
                    query = sortOrder == "ASC"
                        ? query.OrderBy(x => x.Description)
                        : query.OrderByDescending(x => x.Description);
                    break;
                
                case "price":
                    query = sortOrder == "ASC"
                        ? query.OrderBy(x => x.Price)
                        : query.OrderByDescending(x => x.Price);
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
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Item>
        {
            Data = items.Cast<Item>().ToList(),
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task AddAsync(Item item) => await dbset.AddAsync((ServiceItem)item);

    public Task UpdateAsync(Item item)
    {
        dbset.Attach((ServiceItem) item);
        context.Entry(item).State = EntityState.Modified; 
        return Task.CompletedTask;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ServiceItem? existingItem = await GetByIdAsync(id);
        if (existingItem is null) return false;
        existingItem.IsActive = false;
        return true;
    }

    public async Task<bool> NameExistsAsync(string name)
    {
        return await dbset.Where(x => x.IsActive).AnyAsync(x => x.Name == name);
    }

    public async Task<bool> NameExistsForOtherAsync(Guid uuid, string name)
    {
        return await dbset.Where(x => x.IsActive).AnyAsync(x =>
            x.Name == name &&
            x.Uuid != uuid);
    }
}