using Invoicing_Backend.Data;
using Invoicing_Backend.Repositories.Items;
using Invoicing_Backend.Repositories.VatRates;

namespace Invoicing_Backend.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly InvoicingAppDbContext _context;
    public CustomerRepository CustomerRepository { get; }
    public RegionRepository RegionRepository { get; }
    public TaxOfficeRepository TaxOfficeRepository { get; }
    public VatRateRepository VatRateRepository { get; }
    public ItemRepository ItemRepository { get; }
    
    public UnitOfWork(InvoicingAppDbContext context)
    {
        _context = context;
        CustomerRepository = new CustomerRepository(context);
        RegionRepository = new RegionRepository(context);
        TaxOfficeRepository = new TaxOfficeRepository(context);
        VatRateRepository = new VatRateRepository(context);
        ItemRepository = new ItemRepository(context);
    }
    
    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}