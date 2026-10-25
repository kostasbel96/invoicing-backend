using Invoicing_Backend.DTOs.Item;
using Invoicing_Backend.Models;

namespace Invoicing_Backend.Services.Items;

public interface IItemService
{
    Task<PaginatedResult<ItemReadOnlyDto>> GetPaginatedItemsAsync(int pageNumber, 
        int pageSize, string searchTerm,
        string sortField, string sortOrder);
    Task<ItemReadOnlyDto> AddAsync(ItemInsertDto dto);
    Task<ItemReadOnlyDto?> UpdateAsync(Guid uuid, ItemUpdateDto dto);
    Task<bool> DeleteAsync(Guid uuid);
    Task<ItemReadOnlyDto?> GetItemByUuidAsync(Guid uuid);
    
    Task<ItemReadOnlyDto?> GetItemByIdAsync(int id);

}