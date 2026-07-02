namespace Invoicing_Backend.Exceptions;

public class CustomerPhoneAlreadyExistsException : AppException
{
    private static readonly string MESSAGE = "Customer phone already exists";
    private static readonly string CODE = "phoneAlreadyExists";
    public CustomerPhoneAlreadyExistsException() : base(CODE, MESSAGE)
    {
    }
}