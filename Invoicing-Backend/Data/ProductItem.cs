namespace Invoicing_Backend.Data;

public class ProductItem : Item
{
    public Unit Unit { get; set; }
    public decimal? Quantity { get; set; }
    
}