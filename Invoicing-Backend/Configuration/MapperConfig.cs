using AutoMapper;
using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs;
using Invoicing_Backend.DTOs.Item;
using Invoicing_Backend.DTOs.Item.ServiceItem;
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
        
        //ITEM - READ
        CreateMap<Item, ItemReadOnlyDto>()
            .Include<ProductItem, ProductReadOnlyDto>()
            .Include<ServiceItem, ServiceItemReadOnlyDto>();
        CreateMap<ProductItem, ProductReadOnlyDto>();
        CreateMap<ServiceItem, ServiceItemReadOnlyDto>();

        //ITEM - INSERT (concrete μόνο, ο runtime τύπος βρίσκει το σωστό map)
        CreateMap<ProductInsertDto, ProductItem>().IgnoreBaseEntity();
        CreateMap<ServiceItemInsertDto, ServiceItem>().IgnoreBaseEntity();

        //ITEM - UPDATE (base + Include, γιατί το Map(dto, item) είναι static-typed)
        CreateMap<ItemUpdateDto, ProductItem>()
            .IgnoreBaseEntity()
            .Include<ProductUpdateDto, ProductItem>();
        CreateMap<ProductUpdateDto, ProductItem>();
        
        CreateMap<ItemUpdateDto, ServiceItem>()
            .IgnoreBaseEntity()
            .Include<ServiceItemUpdateDto, ServiceItem>();
        CreateMap<ServiceItemUpdateDto, ServiceItem>();
    }
    
}

public static class MapperExtensions
{
    public static IMappingExpression<TSrc, TDest> IgnoreBaseEntity<TSrc, TDest>(
        this IMappingExpression<TSrc, TDest> map) where TDest : Item
    {
        return map
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Uuid, o => o.Ignore())
            .ForMember(d => d.InsertedAt, o => o.Ignore())
            .ForMember(d => d.ModifiedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore())
            .ForMember(d => d.InvoiceItems, o => o.Ignore());
    }
}