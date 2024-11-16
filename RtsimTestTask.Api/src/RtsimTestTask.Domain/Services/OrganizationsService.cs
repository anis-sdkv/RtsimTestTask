using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.Abstractions.Services;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Domain.Services;

public class OrganizationsService(IOrganizationsRepository organizations, IUsersRepository users)
    : IOrganizationsService
{
    public Task<Guid> CreateOrganizationAsync(
        CreateOrganizationDto createData,
        CancellationToken cancellationToken
    ) => organizations.CreateAsync(createData, cancellationToken);

    public Task<IEnumerable<DomainOrganization>> GetAllAsync(CancellationToken cancellationToken) =>
        organizations.GetAllAsync(cancellationToken);

    public Task<DomainOrganization?> GetOrganizationByIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken
    ) => organizations.GetByIdAsync(organizationId, cancellationToken);

    public Task<DomainOrganization?> GetOrganizationByNameAsync(
        string name,
        CancellationToken cancellationToken
    ) => organizations.GetByNameAsync(name, cancellationToken);

    public Task<IEnumerable<DomainOrganization>> SearchOrganizationsAsync(
        SearchOrganizationDto searchParams,
        CancellationToken cancellationToken
    ) => organizations.SearchAsync(searchParams, cancellationToken);

    public Task<IEnumerable<DomainUser>> GetUsersByOrganizationIdAsync(
        Guid organizationId,
        CancellationToken cancellationToken
    ) => users.GetUsersByOrganizationAsync(organizationId, cancellationToken);

    public Task UpdateOrganizationAsync(
        UpdateOrganizationDto dto,
        CancellationToken cancellationToken
    ) => organizations.UpdateAsync(dto, cancellationToken);

    public Task DeleteOrganizationAsync(
        Guid organizationId,
        CancellationToken cancellationToken
    ) => organizations.RemoveAsync(organizationId, cancellationToken);
}