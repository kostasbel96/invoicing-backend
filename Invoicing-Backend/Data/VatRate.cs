namespace Invoicing_Backend.Data;

public class VatRate : BaseEntity
{

    public string Name { get; set; } = null!;
    public decimal Rate { get; set; }

}