using System.ComponentModel.DataAnnotations;
using Invoicing_Backend.Data;

namespace Invoicing_Backend.DTOs.Item;

public class ProductInsertDto
{
    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Item name must be between 2 and 50 characters.")]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "The {0} field is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "The {0} field is required.")]
    public Unit Unit { get; set; }

    [Required(ErrorMessage = "The {0} field is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
    public decimal Quantity { get; set; }
}