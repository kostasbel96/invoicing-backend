using Invoicing_Backend.DTOs.TaxOffice;

namespace Invoicing_Backend.Services;

public interface ITaxOfficeService
{
    Task<List<TaxOfficeReadOnlyDto>> GetAllTaxOfficesAsync();
}