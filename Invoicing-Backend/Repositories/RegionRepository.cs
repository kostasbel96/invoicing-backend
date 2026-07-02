using Invoicing_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Invoicing_Backend.Repositories;

public class RegionRepository : BaseRepository<Region>, IRegionRepository
{
    public RegionRepository(InvoicingAppDbContext context) : base(context)
    {
    }

    public async Task<Region?> GetRegionByCodeAsync(string code) => await dbset
        .FirstOrDefaultAsync(x => EF.Property<string>(x, "Code") == code);
}