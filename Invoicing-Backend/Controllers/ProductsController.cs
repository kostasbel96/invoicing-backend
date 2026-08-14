using Invoicing_Backend.DTOs.Item;
using Invoicing_Backend.Exceptions;
using Invoicing_Backend.Models;
using Invoicing_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing_Backend.Controllers;

public class ProductsController : BaseController
{
    public ProductsController(IApplicationService applicationService) : base(applicationService)
    {
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ProductReadOnlyDto>> AddProduct([FromBody] ProductInsertDto productInsertDto)
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

        var returnedItem = await _applicationService.ProductService.AddAsync(productInsertDto);

        return CreatedAtAction(nameof(GetItemById), new { id = returnedItem.Id }, returnedItem);
    }

    [HttpPatch("Update/{uuid:guid}")]
    public async Task<ActionResult<ProductReadOnlyDto>> UpdateItem(
        [FromRoute] Guid uuid,
        [FromBody] ProductUpdateDto productUpdateDto)
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

        if (await _applicationService.ProductService.GetItemByUuidAsync(uuid) is null)
        {
            return NotFound();
        }

        var returnedItem = await _applicationService.ProductService.UpdateAsync(uuid, productUpdateDto);

        return Ok(returnedItem);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadOnlyDto>> GetItemById([FromRoute] int id)
    {
        var item = await _applicationService.ProductService.GetItemByIdAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet("by-uuid/{uuid:guid}")]
    public async Task<ActionResult<ProductReadOnlyDto>> GetItemByUuid([FromRoute] Guid uuid)
    {
        var item = await _applicationService.ProductService.GetItemByUuidAsync(uuid);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ProductReadOnlyDto>>> GetItems(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string search = "",
        [FromQuery] string sortField = "Id",
        [FromQuery] string sortOrder = "ASC")
    {
        var items = await _applicationService.ProductService
            .GetPaginatedProductsAsync(page, pageSize, search, sortField, sortOrder);

        return Ok(items);
    }

    [HttpDelete("{uuid:guid}")]
    public async Task<ActionResult<bool>> DeleteItem(Guid uuid)
    {
        bool deleted = await _applicationService.ProductService.DeleteAsync(uuid);

        if (!deleted)
        {
            return NotFound(false);
        }

        return Ok(true);
    }
}