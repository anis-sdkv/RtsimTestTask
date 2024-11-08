using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Repositories;

public interface IOrganizationsRepository : IRepository<Organization, Guid>
{
    Task<IEnumerable<Organization>> Search(SearchOrganizationDto searchParams, CancellationToken cancellationToken);
}