namespace Invoicing_Backend.Exceptions;

public class ValidationException : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }
    public string Code { get; init; }

    public ValidationException(IReadOnlyDictionary<string, string[]> errors, String message, String code) : base(message)
    {
        Errors = errors;
        Code = code;
    }
}