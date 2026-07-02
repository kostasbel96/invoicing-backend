namespace Invoicing_Backend.Data;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public DateTime InsertedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    
    public bool IsActive { get; set; }
}