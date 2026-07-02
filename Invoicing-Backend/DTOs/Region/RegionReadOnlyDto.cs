namespace Invoicing_Backend.DTOs;

public class RegionReadOnlyDto : BaseReadOnlyDto
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}