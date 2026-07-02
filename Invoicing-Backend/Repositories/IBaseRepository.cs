namespace Invoicing_Backend.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByUuidAsync(Guid uuid);
    Task<int> GetCountAsync();
}