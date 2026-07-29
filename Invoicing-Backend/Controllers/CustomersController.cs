using AutoMapper;
using Invoicing_Backend.DTOs;
using Invoicing_Backend.Exceptions;
using Invoicing_Backend.Models;
using Invoicing_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing_Backend.Controllers;

public class CustomersController : BaseController
{
    public CustomersController(IApplicationService applicationService) : base(applicationService)
    {
    }

    [HttpPost("Add")]
    public async Task<ActionResult<CustomerReadOnlyDto>> AddCustomer([FromBody] CustomerInsertDto customerInsertDto)
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
        var returnedCustomer =  await _applicationService.CustomerService.AddAsync(customerInsertDto);
        return CreatedAtAction(nameof(GetCustomerById), new { id = returnedCustomer.Id }, returnedCustomer);
    }

    [HttpPatch("Update/{uuid::guid}")]
    public async Task<ActionResult<CustomerReadOnlyDto>> UpdateCustomer([FromRoute] Guid uuid, 
        [FromBody] CustomerUpdateDto customerUpdateDto)
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
        
        if (await _applicationService.CustomerService.GetCustomerByUuidAsync(uuid) is null)
        {
            return NotFound();
        }
        
        var returnedCustomer =  await _applicationService.CustomerService.UpdateAsync(uuid, customerUpdateDto);
        return Ok(returnedCustomer);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerReadOnlyDto>> GetCustomerById([FromRoute] int id)
    {
        var customer = await _applicationService.CustomerService.GetCustomerByIdAsync(id);
        if (customer is null) return NotFound();
        return Ok(customer);
    }
    
    [HttpGet("by-uuid/{uuid::guid}")]
    public async Task<ActionResult<CustomerReadOnlyDto>> GetCustomerByUuid([FromRoute] Guid uuid)
    {
        var customer = await _applicationService.CustomerService.GetCustomerByUuidAsync(uuid);
        if (customer is null) return NotFound();
        return Ok(customer);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<CustomerReadOnlyDto>>> GetCustomers([FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string search = "",
        [FromQuery] string sortField = "Id",
        [FromQuery] string sortOrder = "ASC")
    {
        var customers = await _applicationService.CustomerService
            .GetPaginatedCustomersAsync(page, pageSize, search, sortField, sortOrder);
        return Ok(customers);
    }
    
    [HttpDelete("{uuid:guid}")]
    public async Task<ActionResult<bool>> DeleteCustomer(Guid uuid)
    {
        bool deleted = await _applicationService.CustomerService.DeleteAsync(uuid);

        if (!deleted)
        {
            return NotFound(false);
        }

        return Ok(true);
    }
}