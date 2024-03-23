using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoemiPOS.Application.Exceptions;
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
        ExceptionDetails exceptionDetails = GetExceptionDetails(exception);

        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

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

    private ExceptionDetails GetExceptionDetails(Exception exception)
    {
        switch (exception)
        {
            case ValidationException validationException:
                return new ExceptionDetails(StatusCodes.Status400BadRequest, "ValidationFailure", "Validation Error", validationException.Message, validationException.Errors);

            case DbUpdateException dbUpdateException when dbUpdateException.InnerException is PostgresException postgresException:
                return MapPostgresException(postgresException);

            default:
                return new ExceptionDetails(StatusCodes.Status500InternalServerError, "ServerError", "Server Error", exception.Message, null);
        }
    }

    private static ExceptionDetails MapPostgresException(PostgresException postgresException)
    {
        if (PostgresErrorMap.TryGetValue(postgresException.SqlState, out var errorInfo))
        {
            return new ExceptionDetails(
                errorInfo.StatusCode,
                "PostgreSQL Error",
                errorInfo.Title,
                errorInfo.Detail,
                new[] { postgresException.MessageText });
        }
        else
        {
            return new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "PostgreSQL Error",
                "Unexpected Error",
                $"An unexpected database error has occurred: {postgresException.Message}",
                new[] { postgresException.MessageText });
        }
    }

    private static readonly Dictionary<string, (int StatusCode, string Title, string Detail)> PostgresErrorMap = new()
{
    { PostgresErrorCodes.UniqueViolation, (StatusCodes.Status409Conflict, "Unique Constraint Violation", "A record with the provided identifier already exists.") },
    { PostgresErrorCodes.ForeignKeyViolation, (StatusCodes.Status400BadRequest, "Foreign Key Violation", "The operation violates a foreign key constraint.") },
};

    internal sealed record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}

