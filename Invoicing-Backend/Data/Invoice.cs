namespace Invoicing_Backend.Data;

public class Invoice : BaseEntity
{
    public string InvoiceNumber { get; set; } = null!;
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    public decimal Subtotal { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TaxAmount { get; set; } 
    public decimal Total { get; set; } 
    
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    
    public List<InvoiceItem> Items { get; set; } = new();
}