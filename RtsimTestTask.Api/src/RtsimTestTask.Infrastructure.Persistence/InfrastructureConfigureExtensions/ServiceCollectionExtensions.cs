using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RtsimTestTask.Domain.Abstractions.Authentication;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Infrastructure.Persistence.Authentication;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;
using RtsimTestTask.Infrastructure.Persistence.Options;
using RtsimTestTask.Infrastructure.Persistence.Repositories;

namespace RtsimTestTask.Infrastructure.Persistence.InfrastructureConfigureExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot config)
    {
        services.Configure<InfrastructureOptions>(config.GetRequiredSection(nameof(InfrastructureOptions)));
        services.ConfigureIdentityAuthentication(config);

        services.AddScoped<IAccountManager, AccountManager>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddRepositories();

        services.AddDbContext<ApplicationDbContext>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationsRepository, OrganizationsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }

    private static IServiceCollection ConfigureIdentityAuthentication(
        this IServiceCollection services,
        IConfigurationRoot config)
    {
        var jwtConfig = config.GetRequiredSection(nameof(JwtOptions));
        services.Configure<JwtOptions>(jwtConfig);
        var jwtOptions = jwtConfig.Get<JwtOptions>() ?? throw new ArgumentNullException();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudiences = [jwtOptions.Audience],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                    ValidateLifetime = true,
                };
            });
        services.AddAuthorization();

        services.AddIdentityCore<UserEntity>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        });
        return services;
    }
}