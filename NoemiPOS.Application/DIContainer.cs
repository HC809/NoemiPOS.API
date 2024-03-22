using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using NoemiPOS.Application.Abstractions.Behaviors;

namespace NoemiPOS.Application;
public static class DIContainer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DIContainer).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DIContainer).Assembly, includeInternalTypes: true);

        return services;
    }
}
