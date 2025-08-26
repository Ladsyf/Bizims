using Bizims.Application.Businesses;
using Bizims.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Bizims.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services)
    {
        services
            .AddUserDI()
            .AddBusinessDI();

        return services;
    }
}