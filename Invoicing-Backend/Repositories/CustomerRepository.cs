using Invoicing_Backend.Data;
using Invoicing_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoicing_Backend.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    
    public CustomerRepository(InvoicingAppDbContext context) : base(context)
    {
    }

    public async Task<PaginatedResult<Customer>> GetPaginatedCustomersAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await context.Customers
            .Where(x => x.IsActive)
            .CountAsync();

        int skip = (pageNumber - 1) * pageSize;
            
        var customers = await context.Customers
            .Where(x => x.IsActive)
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Customer>
        {
            Data = customers,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

    }

    public async Task AddAsync(Customer customer) => await dbset.AddAsync(customer);

    public Task UpdateAsync(Customer customer)
    {
        dbset.Attach(customer);
        context.Entry(customer).State = EntityState.Modified; 
        return Task.CompletedTask;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Customer? existingCustomer = await GetByIdAsync(id);
        if (existingCustomer is null) return false;
        dbset.Remove(existingCustomer);
        return true;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await dbset.AnyAsync(x => x.Email == email);
    }
    
    public async Task<bool> EmailExistsForOtherAsync(Guid uuid, string email)
    {
        return await dbset.AnyAsync(x =>
            x.Email == email &&
            x.Uuid != uuid);
    }
    
    public async Task<bool> PhoneExistsAsync(string phone)
    {
        return await dbset.AnyAsync(x => x.Phone == phone);
    }
    
    public async Task<bool> PhoneExistsForOtherAsync(Guid uuid, string phone)
    {
        return await dbset.AnyAsync(x =>
            x.Phone == phone &&
            x.Uuid != uuid);
    }
}