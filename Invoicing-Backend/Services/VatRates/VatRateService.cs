using AutoMapper;
using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs.VatRate;
using Invoicing_Backend.Repositories;

namespace Invoicing_Backend.Services.VatRates;

public class VatRateService : IVatRateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VatRateService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<VatRateReadOnlyDto>> GetAllVatRatesAsync()
    {
        IEnumerable<VatRate> vatRates = await _unitOfWork.VatRateRepository.GetAllAsync();
        return _mapper.Map<List<VatRateReadOnlyDto>>(vatRates);
    }
}