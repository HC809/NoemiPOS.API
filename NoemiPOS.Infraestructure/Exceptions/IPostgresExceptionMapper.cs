using Npgsql;

namespace NoemiPOS.Infraestructure.Exceptions;
public interface IPostgresExceptionMapper
{
    PostgresExceptionDetails Map(PostgresException postgresException);
}
