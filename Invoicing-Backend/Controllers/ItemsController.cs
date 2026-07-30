using Invoicing_Backend.DTOs.Item;
using Invoicing_Backend.Exceptions;
using Invoicing_Backend.Models;
using Invoicing_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing_Backend.Controllers;

public class ItemsController : BaseController
{
    public ItemsController(IApplicationService applicationService) : base(applicationService)
    {
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ItemReadOnlyDto>> AddItem([FromBody] ItemInsertDto itemInsertDto)
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

        var returnedItem = await _applicationService.ItemService.AddAsync(itemInsertDto);

        return CreatedAtAction(nameof(GetItemById), new { id = returnedItem.Id }, returnedItem);
    }

    [HttpPatch("Update/{uuid:guid}")]
    public async Task<ActionResult<ItemReadOnlyDto>> UpdateItem(
        [FromRoute] Guid uuid,
        [FromBody] ItemUpdateDto itemUpdateDto)
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

        if (await _applicationService.ItemService.GetItemByUuidAsync(uuid) is null)
        {
            return NotFound();
        }

        var returnedItem = await _applicationService.ItemService.UpdateAsync(uuid, itemUpdateDto);

        return Ok(returnedItem);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemReadOnlyDto>> GetItemById([FromRoute] int id)
    {
        var item = await _applicationService.ItemService.GetItemByIdAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet("by-uuid/{uuid:guid}")]
    public async Task<ActionResult<ItemReadOnlyDto>> GetItemByUuid([FromRoute] Guid uuid)
    {
        var item = await _applicationService.ItemService.GetItemByUuidAsync(uuid);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ItemReadOnlyDto>>> GetItems(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string search = "",
        [FromQuery] string sortField = "Id",
        [FromQuery] string sortOrder = "ASC")
    {
        var items = await _applicationService.ItemService
            .GetPaginatedItemsAsync(page, pageSize, search, sortField, sortOrder);

        return Ok(items);
    }

    [HttpDelete("{uuid:guid}")]
    public async Task<ActionResult<bool>> DeleteItem(Guid uuid)
    {
        bool deleted = await _applicationService.ItemService.DeleteAsync(uuid);

        if (!deleted)
        {
            return NotFound(false);
        }

        return Ok(true);
    }
}