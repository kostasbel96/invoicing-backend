using Invoicing_Backend.Repositories.Items;
using Invoicing_Backend.Repositories.VatRates;

namespace Invoicing_Backend.Repositories;

public interface IUnitOfWork
{
    CustomerRepository CustomerRepository { get; }
    RegionRepository RegionRepository { get; }
    TaxOfficeRepository TaxOfficeRepository { get; }
    VatRateRepository VatRateRepository { get; }
    ItemRepository ItemRepository { get; }
    Task<bool> SaveAsync();
}