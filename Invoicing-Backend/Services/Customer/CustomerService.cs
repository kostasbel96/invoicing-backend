using AutoMapper;
using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs;
using Invoicing_Backend.Exceptions;
using Invoicing_Backend.Models;
using Invoicing_Backend.Repositories;

namespace Invoicing_Backend.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CustomerService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<PaginatedResult<CustomerReadOnlyDto>> GetPaginatedCustomersAsync(int pageNumber, 
        int pageSize, 
        string searchTerm,
        string sortField,
        string sortOrder)
    {
        PaginatedResult<Customer> result = await _unitOfWork.CustomerRepository.GetPaginatedCustomersAsync(pageNumber, 
            pageSize, 
            searchTerm,
            sortField,
            sortOrder);

        return new PaginatedResult<CustomerReadOnlyDto>
        {
            Data = _mapper.Map<List<CustomerReadOnlyDto>>(result.Data),
            TotalRecords = result.TotalRecords,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }

    public async Task<CustomerReadOnlyDto> AddAsync(CustomerInsertDto dto)
    {
        try
        {
            if (dto.Vat != null && await _unitOfWork.CustomerRepository.VatExistsAsync(dto.Vat))
            {
                _logger.LogWarning("Vat already exists: {Vat}", dto.Vat);
                throw new CustomerFieldAlreadyExistsException("VatAlreadyExists", 
                    "Vat " + dto.Vat + " already exists");
            }

            if (dto.Email != null && await _unitOfWork.CustomerRepository.EmailExistsAsync(dto.Email))
            {
                _logger.LogWarning("Email already exists: {Email}", dto.Email);
                throw new CustomerFieldAlreadyExistsException("EmailAlreadyExists", 
                    "Email " + dto.Email + " already exists");
            }
            if (await _unitOfWork.CustomerRepository.EmailExistsAsync(dto.Phone))
            {
                _logger.LogWarning("Phone already exists: {Phone}", dto.Phone);
                throw new CustomerFieldAlreadyExistsException("PhoneAlreadyExists", 
                    "Phone " + dto.Phone + " already exists");
            }
            Customer customer = _mapper.Map<Customer>(dto);
            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Customer {CustomerId} added successfully", customer.Id);
            return _mapper.Map<CustomerReadOnlyDto>(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding customer!");
            throw;
        }
    }

    public async Task<CustomerReadOnlyDto?> UpdateAsync(Guid uuid, CustomerUpdateDto dto)
    {
        try
        {
            if (dto.Vat != null && await _unitOfWork.CustomerRepository.VatExistsForOtherAsync(uuid, dto.Vat))
            {
                _logger.LogWarning("Vat already exists: {Vat}", dto.Vat);
                throw new CustomerFieldAlreadyExistsException("VatAlreadyExists",
                    "Vat " + dto.Vat + " already exists");
            }

            if (dto.Email != null && await _unitOfWork.CustomerRepository.EmailExistsForOtherAsync(uuid, dto.Email))
            {
                _logger.LogWarning("Email already exists: {Email}", dto.Email);
                throw new CustomerFieldAlreadyExistsException("EmailAlreadyExists", 
                    "Email " + dto.Email + " already exists");
            }
            if (dto.Phone != null && await _unitOfWork.CustomerRepository.EmailExistsForOtherAsync(uuid, dto.Phone))
            {
                _logger.LogWarning("Email already exists: {Phone}", dto.Phone);
                throw new CustomerFieldAlreadyExistsException("PhoneAlreadyExists", 
                    "Phone " + dto.Phone + " already exists");
            }

            Customer? customer = await _unitOfWork.CustomerRepository.GetByUuidAsync(uuid);
            if (customer is null) return null;
            _mapper.Map(dto, customer);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Customer {CustomerId} updated successfully", customer.Id);
            return _mapper.Map<CustomerReadOnlyDto>(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating customer with Uuid {Uuid}", uuid);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid uuid)
    {
        try
        {
            Customer? customer = await _unitOfWork.CustomerRepository.GetByUuidAsync(uuid);
            if (customer is null) return false;
            customer.IsActive = false;
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Customer with Uuid {Uuid} deleted", uuid);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting customer with Uuid {Uuid}", uuid);
            throw;
        }
    }

    public async Task<CustomerReadOnlyDto?> GetCustomerByUuidAsync(Guid uuid)
    {
        try
        {
            Customer? customer = await _unitOfWork.CustomerRepository
                .GetByUuidAsync(uuid, x => x.Region);
            if (customer is null)
            {
                _logger.LogWarning("Customer with Uuid {Uuid} not found", uuid);
                return null;
            }

            _logger.LogInformation("Customer with Uuid {Uuid} found", uuid);
            return _mapper.Map<CustomerReadOnlyDto>(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error get customer with Uuid {Uuid}", uuid);
            throw;
        }
    }

    public async Task<CustomerReadOnlyDto?> GetCustomerByIdAsync(int id)
    {
        Customer? customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
        if (customer is null) return null;
        return _mapper.Map<CustomerReadOnlyDto>(customer);
    }
}