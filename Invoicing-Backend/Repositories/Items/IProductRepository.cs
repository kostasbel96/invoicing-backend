using Invoicing_Backend.Data;
using Invoicing_Backend.Models;

namespace Invoicing_Backend.Repositories.Items;

public interface IProductRepository
{
    Task<PaginatedResult<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize, 
        string searchTerm, string sortField, string sortOrder);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
    Task<bool> NameExistsAsync(string name);
    Task<bool> NameExistsForOtherAsync(Guid uuid, string name);

}