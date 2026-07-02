namespace Invoicing_Backend.Repositories;

public interface IUnitOfWork
{
    CustomerRepository CustomerRepository { get; }
    RegionRepository RegionRepository { get; }
    Task<bool> SaveAsync();
}