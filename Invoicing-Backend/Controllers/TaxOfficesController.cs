using Invoicing_Backend.DTOs.TaxOffice;
using Invoicing_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing_Backend.Controllers;

public class TaxOfficesController : BaseController
{
    public TaxOfficesController(IApplicationService applicationService) : base(applicationService)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TaxOfficeReadOnlyDto>>> GetTaxOffices()
    {
        var regions = await _applicationService.TaxOfficeService.GetAllTaxOfficesAsync();
        if (regions.Count == 0) return NotFound();
        return Ok(regions);
    }
}