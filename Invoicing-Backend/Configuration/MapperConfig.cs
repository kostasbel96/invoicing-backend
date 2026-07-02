using AutoMapper;
using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs;

namespace Invoicing_Backend.Configuration;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        //CUSTOMER
        CreateMap<Customer, CustomerReadOnlyDto>();
        CreateMap<Customer, CustomerInsertDto>().ReverseMap();
        CreateMap<Customer, CustomerUpdateDto>().ReverseMap();
        
        //REGION
        CreateMap<Region, RegionReadOnlyDto>();
    }
    
}