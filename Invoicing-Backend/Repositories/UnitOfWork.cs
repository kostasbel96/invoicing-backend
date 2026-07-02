using Invoicing_Backend.Data;

namespace Invoicing_Backend.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly InvoicingAppDbContext _context;
    public CustomerRepository CustomerRepository { get; }
    public RegionRepository RegionRepository { get; }
    
    public UnitOfWork(InvoicingAppDbContext context)
    {
        _context = context;
        CustomerRepository = new CustomerRepository(context);
        RegionRepository = new RegionRepository(context);
    }
    
    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}