namespace Invoicing_Backend.Data;

public class Item : BaseEntity
{
    public string Name { get; set; } = null!;
    public ItemType ItemType { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Unit Unit { get; set; } 
    public int Quantity { get; set; }
    public List<InvoiceItem> InvoiceItems { get; set; } = new();
}