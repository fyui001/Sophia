namespace Sophia.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        return services;
    }
}
