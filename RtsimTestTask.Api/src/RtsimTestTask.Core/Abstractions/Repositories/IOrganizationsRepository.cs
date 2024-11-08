using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Repositories;

public interface IOrganizationsRepository : IRepository<DomainOrganization, Guid>
{
    Task<IEnumerable<DomainOrganization>> Search(SearchOrganizationDto searchParams, CancellationToken cancellationToken);
}