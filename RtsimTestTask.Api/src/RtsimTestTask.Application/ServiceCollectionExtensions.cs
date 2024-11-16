using RtsimTestTask.Domain.Abstractions.Services;
using RtsimTestTask.Domain.Services;

namespace RtsimTestTask.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfigurationRoot config)
    {
        services.AddScoped<IOrganizationsService, OrganizationsService>();
        services.AddScoped<IUsersService, UsersService>();
        return services;
    }
}