namespace Invoicing_Backend.DTOs.TaxOffice;

public class TaxOfficeReadOnlyDto : BaseReadOnlyDto
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}