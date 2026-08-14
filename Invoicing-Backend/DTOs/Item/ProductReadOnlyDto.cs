using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs.VatRate;

namespace Invoicing_Backend.DTOs.Item;

public class ProductReadOnlyDto : BaseReadOnlyDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Unit Unit { get; set; }
    public decimal Quantity { get; set; }
}