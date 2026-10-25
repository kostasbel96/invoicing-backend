using System.ComponentModel.DataAnnotations;
using Invoicing_Backend.Data;

namespace Invoicing_Backend.DTOs.Item;

public class ProductUpdateDto : ItemUpdateDto
{
    
    [Required(ErrorMessage = "The {0} field is required.")]
    public Unit Unit { get; set; }

    [Required(ErrorMessage = "The {0} field is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
    public decimal Quantity { get; set; }
}