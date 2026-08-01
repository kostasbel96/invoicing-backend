using AutoMapper;
using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs.Item;
using Invoicing_Backend.Models;
using Invoicing_Backend.Repositories;

namespace Invoicing_Backend.Services.Items;

public class ItemService : IItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ItemService> _logger;

    public ItemService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ItemService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PaginatedResult<ItemReadOnlyDto>> GetPaginatedItemsAsync(int pageNumber, int pageSize, string searchTerm, string sortField, string sortOrder)
    {
        PaginatedResult<Item> result = await _unitOfWork.ItemRepository
            .GetPaginatedItemsAsync(pageNumber, 
                pageSize, 
                searchTerm,
                sortField,
                sortOrder);

        return new PaginatedResult<ItemReadOnlyDto>
        {
            Data = _mapper.Map<List<ItemReadOnlyDto>>(result.Data),
            TotalRecords = result.TotalRecords,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }

    public async Task<ItemReadOnlyDto> AddAsync(ItemInsertDto dto)
    {
        try
        {
            if (await _unitOfWork.ItemRepository.NameExistsAsync(dto.Name))
            {
                _logger.LogWarning("Item name already exists: {Item Name}", dto.Name);
                throw new ItemFieldAlreadyExistsException("ItemNameAlreadyExists", 
                    "Item Name " + dto.Name + " already exists");
            }
            
            Item item = _mapper.Map<Item>(dto);
            await _unitOfWork.ItemRepository.AddAsync(item);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Item {ItemId} added successfully", item.Id);
            return _mapper.Map<ItemReadOnlyDto>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding item!");
            throw;
        }
    }

    public async Task<ItemReadOnlyDto?> UpdateAsync(Guid uuid, ItemUpdateDto dto)
    {
        try
        {
            if (await _unitOfWork.ItemRepository.NameExistsForOtherAsync(uuid, dto.Name))
            {
                _logger.LogWarning("Item name already exists: {Item Name}", dto.Name);
                throw new ItemFieldAlreadyExistsException("ItemNameAlreadyExists", 
                    "Item Name " + dto.Name + " already exists");
            }

            Item? item = await _unitOfWork.ItemRepository.GetByUuidAsync(uuid);
            if (item is null) return null;
            _mapper.Map(dto, item);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Item {ItemId} updated successfully", item.Id);
            return _mapper.Map<ItemReadOnlyDto>(item);
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
            Item? item = await _unitOfWork.ItemRepository.GetByUuidAsync(uuid);
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

    public async Task<ItemReadOnlyDto?> GetItemByUuidAsync(Guid uuid)
    {
        try
        {
            Item? item = await _unitOfWork.ItemRepository
                .GetByUuidAsync(uuid);
            if (item is null)
            {
                _logger.LogWarning("Item with Uuid {Uuid} not found", uuid);
                return null;
            }

            _logger.LogInformation("Item with Uuid {Uuid} found", uuid);
            return _mapper.Map<ItemReadOnlyDto>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error get item with Uuid {Uuid}", uuid);
            throw;
        }
    }

    public async Task<ItemReadOnlyDto?> GetItemByIdAsync(int id)
    {
        Item? item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
        if (item is null) return null;
        return _mapper.Map<ItemReadOnlyDto>(item);
    }
}