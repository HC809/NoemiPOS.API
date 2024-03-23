using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoemiPOS.Application.Exceptions;
using NoemiPOS.Infraestructure.Exceptions;
using Npgsql;

namespace NoemiPOS.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var exceptionDetails = GetExceptionDetails(context, exception);

        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        await WriteProblemDetailsAsync(context, exceptionDetails);
    }

    private ExceptionDetails GetExceptionDetails(HttpContext context, Exception exception)
    {
        if (exception is DbUpdateException dbUpdateException && dbUpdateException.InnerException is PostgresException postgresException)
            return HandleDbUpdateException(context, postgresException);

        return exception switch
        {
            ValidationException validationException => new ExceptionDetails(StatusCodes.Status400BadRequest, "ValidationFailure", "Validation Error", validationException.Message, validationException.Errors),
            _ => new ExceptionDetails(StatusCodes.Status500InternalServerError, "ServerError", "Server Error", exception.Message, null)
        };
    }

    private ExceptionDetails HandleDbUpdateException(HttpContext context, PostgresException postgresException)
    {
        var scopeFactory = context.RequestServices.GetService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var postgresExceptionMapper = scope.ServiceProvider.GetService<IPostgresExceptionMapper>();
            if (postgresExceptionMapper != null)
            {
                var postgresExceptionDetails = postgresExceptionMapper.Map(postgresException);

                return new ExceptionDetails(postgresExceptionDetails.Status, postgresExceptionDetails.Type, postgresExceptionDetails.Title, postgresExceptionDetails.Detail, postgresExceptionDetails.Errors);
            }
        }

        return new ExceptionDetails(StatusCodes.Status500InternalServerError, "PostgreSQLError", "Unexpected database error", postgresException.Detail, new[] { postgresException.Message });
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, ExceptionDetails exceptionDetails)
    {
        var problemDetails = new ProblemDetails
        {
            Status = exceptionDetails.Status,
            Type = exceptionDetails.Type,
            Title = exceptionDetails.Title,
            Detail = exceptionDetails.Detail,
        };

        if (exceptionDetails.Errors is not null)
        {
            problemDetails.Extensions["errors"] = exceptionDetails.Errors;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exceptionDetails.Status;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    internal sealed record ExceptionDetails(
       int Status,
       string Type,
       string Title,
       string Detail,
       IEnumerable<object>? Errors);
}

