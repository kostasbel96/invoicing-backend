using Invoicing_Backend.Data;
using Invoicing_Backend.Models;

namespace Invoicing_Backend.Repositories.Items;

public interface IItemRepository
{
    Task<PaginatedResult<Item>> GetPaginatedItemsAsync(int pageNumber, int pageSize, 
        string searchTerm, string sortField, string sortOrder);
    Task AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task<bool> DeleteAsync(int id);
    Task<bool> NameExistsAsync(string name);
    Task<bool> NameExistsForOtherAsync(Guid uuid, string name);

}