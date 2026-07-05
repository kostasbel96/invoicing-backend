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
            throw new ValidationException(errors);
        }
        var returnedCustomer =  await _applicationService.CustomerService.AddAsync(customerInsertDto);
        return CreatedAtAction(nameof(GetCustomerById), new { id = returnedCustomer.Id }, returnedCustomer);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerReadOnlyDto>> GetCustomerById([FromRoute] int id)
    {
        var customer = await _applicationService.CustomerService.GetCustomerByIdAsync(id);
        if (customer is null) return NotFound();
        return Ok(customer);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<CustomerReadOnlyDto>>> GetCustomers([FromQuery] int page,
        int pageSize)
    {
        var customers = await _applicationService.CustomerService
            .GetPaginatedCustomersAsync(page, pageSize);
        return Ok(customers);
    }
}