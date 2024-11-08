using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RtsimTestTask.Core.Abstractions.Authentication;
using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Infrastructure.Persistence.Authentication;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;
using RtsimTestTask.Infrastructure.Persistence.Repositories;

namespace RtsimTestTask.Infrastructure.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfigurationRoot config)
    {
        services.AddScoped<IAuthenticationProvider, AuthenticationProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddPersistenceInfrastructure(config);
        return services;
    }

    private static IServiceCollection AddPersistenceInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("ConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        );

        services.AddIdentityCore<UserEntity>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<UserEntity>>();

        return services;
    }
}