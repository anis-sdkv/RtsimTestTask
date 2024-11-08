using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Core.Abstractions.Repositories;

public interface IUsersRepository : IRepository<User, Guid>
{
    Task<IEnumerable<User>> GetUsersByOrganization(Guid organizationId, CancellationToken cancellationToken);
}