namespace Invoicing_Backend.Exceptions;

public class ValidationException : AppException
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }
    private const string Code = "ValidationException";

    public ValidationException(IReadOnlyDictionary<string, string[]> errors) : base(Code, "Validation failed")
    {
        Errors = errors;
    }
}