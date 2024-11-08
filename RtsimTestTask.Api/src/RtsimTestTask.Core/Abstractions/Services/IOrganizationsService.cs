using RtsimTestTask.Core.DataTransferObjects;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Services;

public interface IOrganizationsService
{
    Task CreateOrganizationAsync(DomainOrganization domainOrganization, CancellationToken cancellationToken);

    Task<DomainOrganization> GetOrganizationByIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken);

    Task<IEnumerable<DomainOrganization>> SearchOrganizationsAsync(
        SearchOrganizationDto searchParams,
        CancellationToken cancellationToken);


    Task<IEnumerable<DomainUser>> GetUsersByOrganizationIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken);

    Task UpdateOrganizationAsync(DomainOrganization domainOrganization, CancellationToken cancellationToken);
    Task DeleteOrganizationAsync(Guid organizationId, CancellationToken cancellationToken);
}