using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs.VatRate;

namespace Invoicing_Backend.DTOs.Item;

public class ItemReadOnlyDto : BaseReadOnlyDto
{
    public string Name { get; set; } = null!;
    public ItemType ItemType { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Unit Unit { get; set; }
    public int Quantity { get; set; }
    public VatRateReadOnlyDto VatRate { get; set; }
}