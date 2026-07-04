namespace Invoicing_Backend.Exceptions;

public class CustomerPhoneAlreadyExistsException : AppException
{
    private const string Message = "Customer phone already exists";
    private const string Code = "phoneAlreadyExists";
    public CustomerPhoneAlreadyExistsException() : base(Code, Message)
    {
    }
}