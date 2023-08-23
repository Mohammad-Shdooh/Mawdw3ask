using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorMessage = "An error occurred while processing your request.";

        if (exception is NotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            errorMessage = exception.Message;
        }
        else if (exception is BadRequestException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            errorMessage = exception.Message;
        }

        var errorResponse = new { Message = errorMessage };
        var json = JsonSerializer.Serialize(errorResponse);

        await context.Response.WriteAsync(json);
    }
}
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}