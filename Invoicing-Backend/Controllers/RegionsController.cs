using Invoicing_Backend.DTOs;
using Invoicing_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing_Backend.Controllers;

public class RegionsController : BaseController
{
    public RegionsController(IApplicationService applicationService) : base(applicationService)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<List<RegionReadOnlyDto>>> GetRegions()
    {
        var regions = await _applicationService.RegionService.GetAllRegionsAsync();
        if (regions.Count == 0) return NotFound();
        return Ok(regions);
    }
}