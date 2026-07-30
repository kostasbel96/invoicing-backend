using Invoicing_Backend.Data;

namespace Invoicing_Backend.Repositories.VatRates;


public class VatRateRepository : BaseRepository<VatRate>
{
    
    public VatRateRepository(InvoicingAppDbContext context) : base(context)
    {
    }
}