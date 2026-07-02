namespace Invoicing_Backend.Exceptions;

public class CustomerEmailAlreadyExistsException : AppException
{
    private static readonly string MESSAGE = "Customer email already exists";
    private static readonly string CODE = "emailAlreadyExists";
    public CustomerEmailAlreadyExistsException() : base(CODE, MESSAGE)
    {
    }
}