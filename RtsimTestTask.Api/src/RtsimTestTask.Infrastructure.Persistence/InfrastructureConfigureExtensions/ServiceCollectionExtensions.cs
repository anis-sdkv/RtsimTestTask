using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RtsimTestTask.Domain.Abstractions.Authentication;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.Constants;
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
        var infrastructureConfig = config.GetRequiredSection(nameof(InfrastructureOptions));
        var infrastructureOptions =
            infrastructureConfig.Get<InfrastructureOptions>() ?? throw new ArgumentNullException();
        services.Configure<InfrastructureOptions>(infrastructureConfig);
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
            .AddJwtBearer(jwtOptions.Configure);
        services.AddAuthorization();

        services.AddIdentityCore<UserEntity>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(ConfigureIdentityOptions);
        return services;
    }

    private static void ConfigureIdentityOptions(IdentityOptions options)
    {
        options.Password.RequireDigit = PasswordComplexityContanst.RequiresDigit;
        options.Password.RequireLowercase = PasswordComplexityContanst.RequiresLowercase;
        options.Password.RequireNonAlphanumeric = PasswordComplexityContanst.RequireNonAlphanumeric;
        options.Password.RequireUppercase = PasswordComplexityContanst.RequiresUppercase;
        options.Password.RequiredLength = PasswordComplexityContanst.MinPasswordLength;

        options.User.AllowedUserNameCharacters = UsernameConstants.AllowedUserNameCharacters;
    }
}