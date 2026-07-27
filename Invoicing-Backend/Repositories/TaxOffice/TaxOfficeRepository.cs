using Invoicing_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Invoicing_Backend.Repositories;

public class TaxOfficeRepository : BaseRepository<TaxOffice>, ITaxOfficeRepository
{
    public TaxOfficeRepository(InvoicingAppDbContext context) : base(context)
    {
    }

    public async Task<TaxOffice?> GetTaxOfficeByCodeAsync(string code) => await dbset
        .FirstOrDefaultAsync(x => EF.Property<string>(x, "Code") == code);

}