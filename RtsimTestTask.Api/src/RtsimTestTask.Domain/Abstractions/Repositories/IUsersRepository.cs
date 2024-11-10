using System.Runtime.Intrinsics.Arm;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Domain.Abstractions.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<DomainUser>> GetUsersByOrganization(Guid organizationId, CancellationToken cancellationToken);
    Task<DomainUser> GetUserById(Guid id, CancellationToken cancellationToken);
}