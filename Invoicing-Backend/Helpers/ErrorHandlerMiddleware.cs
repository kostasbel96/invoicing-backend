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
                CustomerEmailAlreadyExistsException => (int) HttpStatusCode.BadRequest,   // 400
                _ => (int) HttpStatusCode.InternalServerError
            };

            var result = exception is AppException appException
                ? JsonSerializer.Serialize(new
                {
                    code = appException.Code,
                    message = appException.Message,
                    statusCode = response.StatusCode
                })
                : JsonSerializer.Serialize(new
                {
                    code = "internalError",
                    message = exception.Message,
                    statusCode = response.StatusCode
                });

            await response.WriteAsync(result);
        }
    }
}