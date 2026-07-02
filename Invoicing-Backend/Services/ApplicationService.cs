using AutoMapper;
using Invoicing_Backend.Repositories;

namespace Invoicing_Backend.Services;

public class ApplicationService : IApplicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerFactory loggerFactory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggerFactory = loggerFactory;
    }

    public CustomerService CustomerService => new(_unitOfWork, _mapper, _loggerFactory.CreateLogger<CustomerService>());

}