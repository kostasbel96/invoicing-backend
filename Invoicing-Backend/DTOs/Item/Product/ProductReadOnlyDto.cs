using Invoicing_Backend.Data;
using Invoicing_Backend.DTOs.VatRate;

namespace Invoicing_Backend.DTOs.Item;

public class ProductReadOnlyDto : ItemReadOnlyDto
{
    public Unit Unit { get; set; }
    public decimal Quantity { get; set; }
}