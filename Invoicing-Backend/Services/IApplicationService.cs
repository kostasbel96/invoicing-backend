namespace Invoicing_Backend.Services;

public interface IApplicationService
{
    CustomerService CustomerService { get; }
    RegionService RegionService { get; }
}