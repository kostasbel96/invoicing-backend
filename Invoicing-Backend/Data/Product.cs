namespace Invoicing_Backend.Data;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; } = null!;
    public int Quantity { get; set; } 
    public bool IsActive { get; set; } = true;
    public List<InvoiceItem> InvoiceItems { get; set; } = new();
}