using System.ComponentModel.DataAnnotations;
using Invoicing_Backend.Data;

namespace Invoicing_Backend.DTOs;

public class CustomerInsertDto
{
    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Firstname must be between 2 and 50 characters.")]
    public string? Firstname { get; set; }
    
    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Lastname must be between 2 and 50 characters.")]
    public string? Lastname { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "The {0} field is required.")]
    [StringLength(15, MinimumLength = 10,
        ErrorMessage = "Phone number must be at least 10 characters and not exceed 15 characters.")]
    public string Phone { get; set; } = null!;
    
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    
    [RegularExpression(@"^$|^\d{10}$", ErrorMessage = "Vat number must contain exactly 10 digits.")]
    public string? Vat { get; set; }
    
    public string? CompanyName { get; set; }
    
    // [Required(ErrorMessage = "The {0} field is required.")]
    public int RegionId { get; set; }
}