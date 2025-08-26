using Bizims.Application.Businesses.Factories;
using Bizims.Application.Businesses.Mappers;
using Bizims.Application.Businesses.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bizims.Application.Businesses;

public static class BusinessDependencies
{
    public static IServiceCollection AddBusinessDI(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddFactories()
            .AddMappers();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBusinessService, BusinessService>();

        return services;
    }

    private static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddScoped<IBusinessFactory, BusinessFactory>();

        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<IBusinessApiMapper, BusinessApiMapper>();

        return services;
    }
}