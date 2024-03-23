using Microsoft.AspNetCore.Http;
using Npgsql;

namespace NoemiPOS.Infraestructure.Exceptions;
public class PostgresExceptionMapper : IPostgresExceptionMapper
{
    private static readonly Dictionary<string, (int StatusCode, string Title, string Detail)> PostgresErrorMap = new()
    {
        { PostgresErrorCodes.UniqueViolation, (StatusCodes.Status409Conflict, "Unique Constraint Violation", "A record with the provided identifier already exists.") },
        { PostgresErrorCodes.ForeignKeyViolation, (StatusCodes.Status400BadRequest, "Foreign Key Violation", "The operation violates a foreign key constraint.") },
        // Agrega más mapeos según sea necesario
    };

    public PostgresExceptionDetails Map(PostgresException postgresException)
    {
        if (PostgresErrorMap.TryGetValue(postgresException.SqlState, out var errorInfo))
        {
            return new PostgresExceptionDetails(
                errorInfo.StatusCode,
                "PostgreSQLError",
                errorInfo.Title,
                errorInfo.Detail,
                new[] { postgresException.MessageText });
        }
        else
        {
            return new PostgresExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "PostgreSQLError",
                "Unexpected Error",
                $"An unexpected database error has occurred: {postgresException.Message}",
                new[] { postgresException.MessageText });
        }
    }
}
