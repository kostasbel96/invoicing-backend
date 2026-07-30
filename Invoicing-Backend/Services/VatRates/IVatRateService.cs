using Invoicing_Backend.DTOs.VatRate;

namespace Invoicing_Backend.Services.VatRates;

public interface IVatRateService
{
    Task<List<VatRateReadOnlyDto>> GetAllVatRatesAsync();
}