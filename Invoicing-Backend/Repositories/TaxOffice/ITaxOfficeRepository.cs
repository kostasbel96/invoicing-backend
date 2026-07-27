using Invoicing_Backend.Data;

namespace Invoicing_Backend.Repositories;

public interface ITaxOfficeRepository
{
    Task<TaxOffice?> GetTaxOfficeByCodeAsync(string code);
}