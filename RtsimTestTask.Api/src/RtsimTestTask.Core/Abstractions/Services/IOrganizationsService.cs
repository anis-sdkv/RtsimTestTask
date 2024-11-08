using RtsimTestTask.Core.DataTransferObjects;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Services;

public interface IOrganizationsService
{
    Task CreateOrganizationAsync(Organization organization, CancellationToken cancellationToken);

    Task<Organization> GetOrganizationByIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken);

    Task<IEnumerable<Organization>> SearchOrganizationsAsync(
        SearchOrganizationDto searchParams,
        CancellationToken cancellationToken);


    Task<IEnumerable<User>> GetUsersByOrganizationIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken);

    Task UpdateOrganizationAsync(Organization organization, CancellationToken cancellationToken);
    Task DeleteOrganizationAsync(Guid organizationId, CancellationToken cancellationToken);
}