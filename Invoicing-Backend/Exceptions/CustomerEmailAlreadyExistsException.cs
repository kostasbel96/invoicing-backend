namespace Invoicing_Backend.Exceptions;

public class CustomerEmailAlreadyExistsException : AppException
{
    private const string Message = "Customer email already exists";
    private const string Code = "emailAlreadyExists";
    public CustomerEmailAlreadyExistsException() : base(Code, Message)
    {
    }
}