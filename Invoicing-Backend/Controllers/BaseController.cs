using Invoicing_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController: ControllerBase
{
    public readonly IApplicationService _applicationService;

    protected BaseController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
}