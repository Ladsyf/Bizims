using Bizims.Application.Users.Factories;
using Bizims.Application.Users.Mappers;
using Bizims.Application.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bizims.Application.Users;

public static class UserDependencies
{
    public static IServiceCollection AddUserDI(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddFactories()
            .AddMappers();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthValidationService, AuthValidationService>();
        services.AddScoped<IMultitenantProvider, MultitenantProvider>();
        services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserValidationService, UserValidationService>();
        services.AddScoped<ICookieService, CookieService>();

        return services;
    }

    private static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddScoped<IUserFactory, UserFactory>();

        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<IUserApiMapper, UserApiMapper>();

        return services;
    }
}