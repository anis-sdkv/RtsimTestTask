using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RtsimTestTask.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfigurationRoot config)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        return services;
    }
}