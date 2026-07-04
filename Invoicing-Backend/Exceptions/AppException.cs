namespace Invoicing_Backend.Exceptions;

public abstract class AppException : Exception
{
    public string Code { get; }

    public AppException(string code, string message) : base(message) 
    {
        Code = code;
    }
}