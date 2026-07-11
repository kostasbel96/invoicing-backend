using System.ComponentModel.DataAnnotations;
using Invoicing_Backend.Data;

namespace Invoicing_Backend.DTOs;

public class CustomerInsertDto
{
    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Firstname must be between 2 and 50 characters.")]
    public string Firstname { get; set; } = null!;

    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Lastname must be between 2 and 50 characters.")]
    public string Lastname { get; set; } = null!;
    
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [Required(ErrorMessage =  "The {0} field is required.")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "The {0} field is required.")]
    [RegularExpression(@"^\d{10,}$", ErrorMessage = "Phone must contain at least 10 digits.")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "The {0} field is required.")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "The {0} field is required.")]
    [RegularExpression(@"^\d{5}$", ErrorMessage = "Postal code must be 5 digits.")]
    public string PostalCode { get; set; } = null!;
    
    [Required(ErrorMessage = "The {0} field is required.")]
    [RegularExpression(@"^\d{9,10}$", ErrorMessage = "VAT must contain 9 or 10 digits only.")]
    public string Vat { get; set; } = null!;
    
    public string? CompanyName { get; set; }

    [Required(ErrorMessage = "The {0} field is required.")]
    public int RegionId { get; set; }
}