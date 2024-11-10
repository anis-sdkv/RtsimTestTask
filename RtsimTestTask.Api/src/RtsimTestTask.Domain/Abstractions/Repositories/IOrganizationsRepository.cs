using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Domain.Abstractions.Repositories;

public interface IOrganizationsRepository
{
    Task<IEnumerable<DomainOrganization>> SearchAsync(
        SearchOrganizationDto searchParams,
        CancellationToken cancellationToken);

    Task<DomainOrganization?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<DomainOrganization?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateOrganizationDto dto, CancellationToken cancellationToken);
    Task<Guid> Create(CreateOrganizationDto dto, CancellationToken cancellationToken);
    Task RemoveAsync(Guid entity, CancellationToken cancellationToken);
}