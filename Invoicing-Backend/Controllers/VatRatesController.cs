using Invoicing_Backend.DTOs.VatRate;
using Invoicing_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing_Backend.Controllers;

public class VatRatesController : BaseController
{
    public VatRatesController(IApplicationService applicationService) : base(applicationService)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<VatRateReadOnlyDto>>> GetVatRates()
    {
        var vatRates = await _applicationService.VatRateService.GetAllVatRatesAsync();
        if (vatRates.Count == 0) return NotFound();
        return Ok(vatRates);
    }
}