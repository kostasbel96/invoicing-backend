using System.Text.Json.Serialization;
using Invoicing_Backend.DTOs.Item.ServiceItem;

namespace Invoicing_Backend.DTOs.Item;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ProductReadOnlyDto), "product")]
[JsonDerivedType(typeof(ServiceItemReadOnlyDto), "service")]
public abstract class ItemReadOnlyDto : BaseReadOnlyDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}