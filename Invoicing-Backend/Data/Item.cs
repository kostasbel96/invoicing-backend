namespace Invoicing_Backend.Data;

public abstract class Item : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public List<InvoiceItem> InvoiceItems { get; set; } = new();
}