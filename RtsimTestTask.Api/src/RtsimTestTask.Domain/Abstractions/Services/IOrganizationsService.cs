using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Domain.Abstractions.Services;

public interface IOrganizationsService
{
    Task<Guid> CreateOrganizationAsync(CreateOrganizationDto createData, CancellationToken cancellationToken);

    Task<DomainOrganization?> GetOrganizationByIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken);

    Task<DomainOrganization?> GetOrganizationByNameAsync(
        string name,
        CancellationToken cancellationToken);

    Task<IEnumerable<DomainOrganization>> SearchOrganizationsAsync(
        SearchOrganizationDto searchParams,
        CancellationToken cancellationToken);


    Task<IEnumerable<DomainUser>> GetUsersByOrganizationIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken);

    Task UpdateOrganizationAsync(UpdateOrganizationDto dto, CancellationToken cancellationToken);
    Task DeleteOrganizationAsync(Guid organizationId, CancellationToken cancellationToken);
}