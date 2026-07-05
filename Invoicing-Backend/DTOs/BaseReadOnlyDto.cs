namespace Invoicing_Backend.DTOs;

public abstract class BaseReadOnlyDto
{
    public int Id { get; set; }
    public Guid Uuid { get; set; }
}