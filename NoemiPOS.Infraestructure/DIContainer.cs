using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Businesses;
using NoemiPOS.Domain.Tenants;
using NoemiPOS.Domain.Users;
using NoemiPOS.Infraestructure.Authentication;
using NoemiPOS.Infraestructure.Authentication.Services;
using NoemiPOS.Infraestructure.Authorization;
using NoemiPOS.Infraestructure.Data;
using NoemiPOS.Infraestructure.Exceptions;
using NoemiPOS.Infraestructure.Multinenacy;
using NoemiPOS.Infraestructure.Repositories;

namespace NoemiPOS.Infraestructure;
public static class DIContainer
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPersistence(services, configuration);
        AddAuthentication(services, configuration);
        AddAuthorization(services);

        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        return services;
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtOptions>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(PoliciesConstants.NoemiSuperAdminPolicy, policy => policy.RequireRole(RolesConstants.NoemiSuperAdmin));
            options.AddPolicy(PoliciesConstants.BusinessAdminPolicy, policy => policy.RequireRole(RolesConstants.BusinessAdmin));
        });
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NoemiDB") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        services.AddSingleton<IPostgresExceptionMapper, PostgresExceptionMapper>();
    }
}
