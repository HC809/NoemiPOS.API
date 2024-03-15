using Microsoft.Extensions.DependencyInjection;

namespace NoemiPOS.Application;
public static class DIContainer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DIContainer).Assembly);
        });

        return services;
    }
}
