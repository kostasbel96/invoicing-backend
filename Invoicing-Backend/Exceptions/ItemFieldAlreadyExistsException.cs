
using Invoicing_Backend.Exceptions;

public class ItemFieldAlreadyExistsException : AppException
{
    public ItemFieldAlreadyExistsException(string code, string message) : base(code, message)
    {
    }
}