namespace Invoicing_Backend.Data;

public class TaxOffice : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}