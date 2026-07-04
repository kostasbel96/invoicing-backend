using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs;

namespace Invoicing_Backend.Services;

public interface IRegionService
{
    Task<List<RegionReadOnlyDto>> GetAllRegionsAsync();
}