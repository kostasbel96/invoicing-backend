namespace Invoicing_Backend.Data;

public class InvoiceItem : BaseEntity
{
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
    
    public int? ProductId { get; set; }
    public Product? Product { get; set; }

    public int? ServiceItemId { get; set; }
    public ServiceItem? ServiceItem { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}