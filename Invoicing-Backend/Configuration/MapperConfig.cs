using AutoMapper;
using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs;
using Invoicing_Backend.DTOs.Item;
using Invoicing_Backend.DTOs.TaxOffice;
using Invoicing_Backend.DTOs.VatRate;

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
        
        //TAX_OFFICE
        CreateMap<TaxOffice, TaxOfficeReadOnlyDto>();
        
        //VAT_RATE
        CreateMap<VatRate, VatRateReadOnlyDto>();
        
        //ITEM
        CreateMap<Item, ItemReadOnlyDto>();
        CreateMap<Item, ItemInsertDto>().ReverseMap();
        CreateMap<Item, ItemUpdateDto>().ReverseMap();
    }
    
}