using Invoicing_Backend.DTOs.Item;
using Invoicing_Backend.Models;

namespace Invoicing_Backend.Services.Items;

public interface IProductService
{
    Task<PaginatedResult<ProductReadOnlyDto>> GetPaginatedProductsAsync(int pageNumber, 
        int pageSize, string searchTerm,
        string sortField, string sortOrder);
    Task<ProductReadOnlyDto> AddAsync(ProductInsertDto dto);
    Task<ProductReadOnlyDto?> UpdateAsync(Guid uuid, ProductUpdateDto dto);
    Task<bool> DeleteAsync(Guid uuid);
    Task<ProductReadOnlyDto?> GetItemByUuidAsync(Guid uuid);
    
    Task<ProductReadOnlyDto?> GetItemByIdAsync(int id);

}