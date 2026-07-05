namespace Invoicing_Backend.Exceptions;

public class CustomerFieldAlreadyExistsException : AppException
{
    public CustomerFieldAlreadyExistsException(string code, string message) : base(code, message)
    {
    }
}