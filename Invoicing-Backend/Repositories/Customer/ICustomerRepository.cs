using Invoicing_Backend.Data;
using Invoicing_Backend.Models;

namespace Invoicing_Backend.Repositories;

public interface ICustomerRepository
{
    Task<PaginatedResult<Customer>> GetPaginatedCustomersAsync(int pageNumber, int pageSize, 
        string searchTerm, string sortField, string sortOrder);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(int id);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> EmailExistsForOtherAsync(Guid uuid, string email);
    Task<bool> PhoneExistsAsync(string email);
    Task<bool> PhoneExistsForOtherAsync(Guid uuid, string email);

}