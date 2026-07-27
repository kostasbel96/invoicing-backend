using Invoicing_Backend.Data;

namespace Invoicing_Backend.Repositories;

public interface IRegionRepository
{
 
    Task<Region?> GetRegionByCodeAsync(string code);
    
}