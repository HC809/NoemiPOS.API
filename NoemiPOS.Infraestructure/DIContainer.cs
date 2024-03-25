using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Tenants;
using NoemiPOS.Infraestructure.Data;
using NoemiPOS.Infraestructure.Exceptions;
using NoemiPOS.Infraestructure.Repositories;

namespace NoemiPOS.Infraestructure;
public static class DIContainer
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NoemiDB") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ITenantRepository, TenantRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        services.AddSingleton<IPostgresExceptionMapper, PostgresExceptionMapper>();

        return services;
    }
}
