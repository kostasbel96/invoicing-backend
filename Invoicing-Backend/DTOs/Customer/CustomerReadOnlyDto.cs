namespace Invoicing_Backend.DTOs;

public class CustomerReadOnlyDto : BaseReadOnlyDto
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string? Email { get; set; }
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string? Vat { get; set; }
    public string? CompanyName { get; set; }
    public RegionReadOnlyDto Region { get; set; } = null!;
    public decimal Balance { get; set; }
}