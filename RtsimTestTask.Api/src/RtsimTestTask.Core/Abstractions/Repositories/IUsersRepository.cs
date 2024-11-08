using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Repositories;

public interface IUsersRepository : IRepository<DomainUser, Guid>
{
    Task<IEnumerable<DomainUser>> GetUsersByOrganization(Guid organizationId, CancellationToken cancellationToken);
}