namespace Invoicing_Backend.DTOs.VatRate;

public class VatRateReadOnlyDto : BaseReadOnlyDto
{
    public string Name { get; set; } = null!;
    public decimal Rate { get; set; }
}