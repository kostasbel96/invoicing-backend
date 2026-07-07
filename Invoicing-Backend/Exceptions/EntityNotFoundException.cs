namespace Invoicing_Backend.Exceptions;

public class EntityNotFoundException : AppException
{
    public EntityNotFoundException(string code, string message) : base(code, message)
    {
    }
}