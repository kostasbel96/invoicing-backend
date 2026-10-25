using Invoicing_Backend.DTOs.Item.ServiceItem;
using Invoicing_Backend.Exceptions;
using Invoicing_Backend.Models;
using Invoicing_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing_Backend.Controllers;

public class ServiceItemsController : BaseController
{
    public ServiceItemsController(IApplicationService applicationService) : base(applicationService)
    {
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ServiceItemReadOnlyDto>> AddServiceItem([FromBody] ServiceItemInsertDto serviceItemInsertDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(e => e.Value!.Errors.Any())
                .ToDictionary(
                    e => e.Key,
                    e => e.Value!.Errors
                        .Select(error => error.ErrorMessage)
                        .ToArray());

            throw new ValidationException(errors, "Validation Error", "ValidationError");
        }

        var returnedItem = await _applicationService.ServiceItemService.AddAsync(serviceItemInsertDto);

        return CreatedAtAction(nameof(GetItemById), new { id = returnedItem.Id }, returnedItem);
    }

    [HttpPatch("Update/{uuid:guid}")]
    public async Task<ActionResult<ServiceItemReadOnlyDto>> UpdateItem(
        [FromRoute] Guid uuid,
        [FromBody] ServiceItemUpdateDto serviceItemUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(e => e.Value!.Errors.Any())
                .ToDictionary(
                    e => e.Key,
                    e => e.Value!.Errors
                        .Select(error => error.ErrorMessage)
                        .ToArray());

            throw new ValidationException(errors, "Validation Error", "ValidationError");
        }

        if (await _applicationService.ServiceItemService.GetItemByUuidAsync(uuid) is null)
        {
            return NotFound();
        }

        var returnedItem = await _applicationService.ServiceItemService.UpdateAsync(uuid, serviceItemUpdateDto);

        return Ok(returnedItem);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceItemReadOnlyDto>> GetItemById([FromRoute] int id)
    {
        var item = await _applicationService.ServiceItemService.GetItemByIdAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet("by-uuid/{uuid:guid}")]
    public async Task<ActionResult<ServiceItemReadOnlyDto>> GetItemByUuid([FromRoute] Guid uuid)
    {
        var item = await _applicationService.ServiceItemService.GetItemByUuidAsync(uuid);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ServiceItemReadOnlyDto>>> GetItems(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string search = "",
        [FromQuery] string sortField = "Id",
        [FromQuery] string sortOrder = "ASC")
    {
        var items = await _applicationService.ServiceItemService
            .GetPaginatedItemsAsync(page, pageSize, search, sortField, sortOrder);

        return Ok(items);
    }

    [HttpDelete("{uuid:guid}")]
    public async Task<ActionResult<bool>> DeleteItem(Guid uuid)
    {
        bool deleted = await _applicationService.ServiceItemService.DeleteAsync(uuid);

        if (!deleted)
        {
            return NotFound(false);
        }

        return Ok(true);
    }
}