using System.Linq.Expressions;
using Invoicing_Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Invoicing_Backend.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly InvoicingAppDbContext context;
    protected readonly DbSet<T> dbset;

    public BaseRepository(InvoicingAppDbContext context)
    {
        this.context = context;
        dbset = context.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync() => await dbset.ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id) => await dbset.FindAsync(id);

    public virtual async Task<T?> GetByUuidAsync(
        Guid uuid,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbset;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(
            x => EF.Property<Guid>(x, "Uuid") == uuid
        );
    }
    
    public virtual async Task<int> GetCountAsync() => await dbset
        .Where(x => EF.Property<bool>(x, "IsActive"))
        .CountAsync();
}