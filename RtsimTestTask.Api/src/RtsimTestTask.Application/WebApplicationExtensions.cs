using Microsoft.Extensions.Options;
using RtsimTestTask.Domain.Abstractions.Authentication;
using RtsimTestTask.Domain.Abstractions.Services;
using RtsimTestTask.Domain.Constants;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.Exceptions;
using RtsimTestTask.Infrastructure.Persistence.Options;

namespace RtsimTestTask.Application;
public static class WebApplicationExtensions
{
    private const string AdminOrganizationName = "Admins";

    public static async Task CreateAdminAccountAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var accountManager = serviceProvider.GetRequiredService<IAccountManager>();
        var adminData = serviceProvider.GetRequiredService<IOptions<InfrastructureOptions>>().Value.AdminAccountData;

        var id = await GetAdminOrganizationIdAndCreateIfNotExist(serviceProvider
            .GetRequiredService<IOrganizationsService>());
        await CreateAdminAccountIfNotExist(id, accountManager, adminData);
    }

    private static async Task CreateAdminAccountIfNotExist(
        Guid organizationId,
        IAccountManager accountManager,
        InfrastructureOptions.AdminData data)
    {
        try
        {
            var user = await accountManager.RegisterAsync(new RegisterUserDto(
                    data.Username,
                    data.Password,
                    organizationId,
                    DomainRoles.Admin),
                DomainRoles.Admin,
                default
            );
        }
        catch (UserAlreadyExistException e)
        {
            // Already created
        }
    }

    private static async Task<Guid> GetAdminOrganizationIdAndCreateIfNotExist(IOrganizationsService organizationsService)
    {
        var adminOrganization = await organizationsService
            .GetOrganizationByNameAsync(AdminOrganizationName, default);
        if (adminOrganization != null)
            return adminOrganization.Id;
        return await organizationsService.CreateOrganizationAsync(new CreateOrganizationDto(AdminOrganizationName),
            default);
    }
}