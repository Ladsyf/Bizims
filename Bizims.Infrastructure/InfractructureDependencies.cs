using Bizims.Domain.Repositories;
using Bizims.Infrastructure.Contexts;
using Bizims.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bizims.Infrastructure;

public static class InfractructureDependencies
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
    {
        services.AddDbContext<RepositoryContext>();

        services.AddScoped<IRepositoryManager, RepositoryManager>();

        return services;
    }
}