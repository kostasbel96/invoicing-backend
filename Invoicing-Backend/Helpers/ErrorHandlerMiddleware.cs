using System.Net;
using System.Text.Json;
using Invoicing_Backend.Exceptions;

namespace Invoicing_Backend.Helpers;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = exception switch
            {
                CustomerEmailAlreadyExistsException => (int) HttpStatusCode.BadRequest, // 400
                ValidationException => (int) HttpStatusCode.BadRequest, //400
                _ => (int) HttpStatusCode.InternalServerError
            };
            string result =  BuildJsonResult(exception, response);
            await response.WriteAsync(result);
        }
    }

    private string BuildJsonResult(Exception exception, HttpResponse response)
    {
        string result;
        switch (exception)
        {
            case CustomerEmailAlreadyExistsException ex:
                result = JsonSerializer.Serialize(new
                    {
                        code = ex.Code,
                        message = ex.Message,
                        statusCode = response.StatusCode
                    }
                );
                break;
            case ValidationException ex:
                result = JsonSerializer.Serialize(new
                    {
                        code = ex.Code,
                        message = ex.Message,
                        errors = ex.Errors,
                        statusCode = response.StatusCode
                    }
                );
                break;
            default:
                result = JsonSerializer.Serialize(new
                    {
                        code = "internalError",
                        message = exception.Message,
                        statusCode = response.StatusCode
                    }
                );
                break;
                
        }
        return result;
    }
}