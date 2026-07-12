using Invoicing_Backend.Data;
using Invoicing_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoicing_Backend.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    
    public CustomerRepository(InvoicingAppDbContext context) : base(context)
    {
    }

    public async Task<PaginatedResult<Customer>> GetPaginatedCustomersAsync(int pageNumber, 
        int pageSize, 
        string searchTerm,
        string sortField,
        string sortOrder)
    {
        var query = context.Customers
            .Where(x => x.IsActive)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x =>
                x.Phone.Contains(searchTerm) ||
                x.Vat.Contains(searchTerm) ||
                x.Firstname.Contains(searchTerm) ||
                x.Lastname.Contains(searchTerm) ||
                x.Email.Contains(searchTerm));
        }
        if (!string.IsNullOrEmpty(sortField))
        {
            switch (sortField.ToLower())
            {
                case "firstname":
                    query = sortOrder == "ASC"
                        ? query.OrderBy(x => x.Firstname)
                        : query.OrderByDescending(x => x.Firstname);
                    break;

                case "lastname":
                    query = sortOrder == "ASC"
                        ? query.OrderBy(x => x.Lastname)
                        : query.OrderByDescending(x => x.Lastname);
                    break;

                case "companyname":
                    query = sortOrder == "ASC"
                        ? query.OrderBy(x => x.CompanyName)
                        : query.OrderByDescending(x => x.CompanyName);
                    break;

                case "balance":
                    query = sortOrder == "ASC"
                        ? query.OrderBy(x => x.Balance)
                        : query.OrderByDescending(x => x.Balance);
                    break;

                case "regionname":
                    query = sortOrder == "ASC"
                        ? query.OrderBy(x => x.Region.Name)
                        : query.OrderByDescending(x => x.Region.Name);
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

        var customers = await query
            .Include(x => x.Region)
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
    
    public async Task<bool> VatExistsAsync(string vat)
    {
        return await dbset.AnyAsync(x => x.Vat == vat);
    }
    
    public async Task<bool> VatExistsForOtherAsync(Guid uuid, string vat)
    {
        return await dbset.AnyAsync(x =>
            x.Vat == vat &&
            x.Uuid != uuid);
    }
}