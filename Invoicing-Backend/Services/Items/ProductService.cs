using AutoMapper;
using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs.Item;
using Invoicing_Backend.Models;
using Invoicing_Backend.Repositories;

namespace Invoicing_Backend.Services.Items;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<ProductReadOnlyDto>> GetPaginatedProductsAsync(int pageNumber, int pageSize, string searchTerm, string sortField, string sortOrder)
    {
        PaginatedResult<Product> result = await _unitOfWork.ProductRepository
            .GetPaginatedProductsAsync(pageNumber, 
                pageSize, 
                searchTerm,
                sortField,
                sortOrder);

        return new PaginatedResult<ProductReadOnlyDto>
        {
            Data = _mapper.Map<List<ProductReadOnlyDto>>(result.Data),
            TotalRecords = result.TotalRecords,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }

    public async Task<ProductReadOnlyDto> AddAsync(ProductInsertDto dto)
    {
        try
        {
            if (await _unitOfWork.ProductRepository.NameExistsAsync(dto.Name))
            {
                _logger.LogWarning("Item name already exists: {Item Name}", dto.Name);
                throw new ItemFieldAlreadyExistsException("ItemNameAlreadyExists", 
                    "Item Name " + dto.Name + " already exists");
            }
            
            Product item = _mapper.Map<Product>(dto);
            await _unitOfWork.ProductRepository.AddAsync(item);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Item {ItemId} added successfully", item.Id);
            return _mapper.Map<ProductReadOnlyDto>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding item!");
            throw;
        }
    }

    public async Task<ProductReadOnlyDto?> UpdateAsync(Guid uuid, ProductUpdateDto dto)
    {
        try
        {
            if (await _unitOfWork.ProductRepository.NameExistsForOtherAsync(uuid, dto.Name))
            {
                _logger.LogWarning("Item name already exists: {Item Name}", dto.Name);
                throw new ItemFieldAlreadyExistsException("ItemNameAlreadyExists", 
                    "Item Name " + dto.Name + " already exists");
            }

            Product? item = await _unitOfWork.ProductRepository.GetByUuidAsync(uuid);
            if (item is null) return null;
            _mapper.Map(dto, item);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Item {ItemId} updated successfully", item.Id);
            return _mapper.Map<ProductReadOnlyDto>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating item with Uuid {Uuid}", uuid);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid uuid)
    {
        try
        {
            Product? item = await _unitOfWork.ProductRepository.GetByUuidAsync(uuid);
            if (item is null) return false;
            item.IsActive = false;
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Item with Uuid {Uuid} deleted", uuid);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting item with Uuid {Uuid}", uuid);
            throw;
        }
    }

    public async Task<ProductReadOnlyDto?> GetItemByUuidAsync(Guid uuid)
    {
        try
        {
            Product? item = await _unitOfWork.ProductRepository
                .GetByUuidAsync(uuid);
            if (item is null)
            {
                _logger.LogWarning("Item with Uuid {Uuid} not found", uuid);
                return null;
            }

            _logger.LogInformation("Item with Uuid {Uuid} found", uuid);
            return _mapper.Map<ProductReadOnlyDto>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error get item with Uuid {Uuid}", uuid);
            throw;
        }
    }

    public async Task<ProductReadOnlyDto?> GetItemByIdAsync(int id)
    {
        Product? item = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        if (item is null) return null;
        return _mapper.Map<ProductReadOnlyDto>(item);
    }
}