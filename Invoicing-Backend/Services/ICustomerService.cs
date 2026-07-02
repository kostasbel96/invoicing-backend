using Invoicing_Backend.DTOs;
using Invoicing_Backend.Models;

namespace Invoicing_Backend.Services;

public interface ICustomerService
{
    Task<PaginatedResult<CustomerReadOnlyDto>> GetPaginatedCustomersAsync(int pageNumber, int pageSize);
    Task<CustomerReadOnlyDto> AddAsync(CustomerInsertDto dto);
    Task<CustomerReadOnlyDto?> UpdateAsync(Guid uuid, CustomerUpdateDto dto);
    Task<bool> DeleteAsync(Guid uuid);
    Task<CustomerReadOnlyDto?> GetCustomerByUuidAsync(Guid uuid);
    
    Task<CustomerReadOnlyDto?> GetCustomerByIdAsync(int id);
    
}