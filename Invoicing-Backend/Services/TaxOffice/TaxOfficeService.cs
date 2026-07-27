using AutoMapper;
using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs.TaxOffice;
using Invoicing_Backend.Repositories;

namespace Invoicing_Backend.Services;

public class TaxOfficeService : ITaxOfficeService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaxOfficeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<TaxOfficeReadOnlyDto>> GetAllTaxOfficesAsync()
    {
        IEnumerable<TaxOffice> taxOffices = await _unitOfWork.TaxOfficeRepository.GetAllAsync();
        return _mapper.Map<List<TaxOfficeReadOnlyDto>>(taxOffices);
    }
}