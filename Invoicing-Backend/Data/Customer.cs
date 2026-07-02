namespace Invoicing_Backend.Data;

public class Customer : BaseEntity
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string? Email { get; set; }
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string? Vat { get; set; }
    public string? CompanyName { get; set; }
    public decimal Balance { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; } = null!;
}