using Invoicing_Backend.Services.Items;
using Invoicing_Backend.Services.VatRates;

namespace Invoicing_Backend.Services;

public interface IApplicationService
{
    CustomerService CustomerService { get; }
    RegionService RegionService { get; }
    TaxOfficeService TaxOfficeService { get; }
    VatRateService VatRateService { get; }
    ItemService ItemService { get; }
}