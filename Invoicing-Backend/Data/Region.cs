namespace Invoicing_Backend.Data;

public class Region : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public List<Customer> Customers { get; set; } = new();
}