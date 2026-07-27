using AutoMapper;
using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs;
using Invoicing_Backend.Repositories;

namespace Invoicing_Backend.Services;

public class RegionService : IRegionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<RegionReadOnlyDto>> GetAllRegionsAsync()
    {
        IEnumerable<Region> regions = await _unitOfWork.RegionRepository.GetAllAsync();
        return _mapper.Map<List<RegionReadOnlyDto>>(regions);
    }
}