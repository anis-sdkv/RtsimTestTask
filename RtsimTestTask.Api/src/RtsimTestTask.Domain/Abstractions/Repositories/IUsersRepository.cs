using System.Runtime.Intrinsics.Arm;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Domain.Abstractions.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<DomainUser>> GetUsersByOrganizationAsync(Guid organizationId, CancellationToken cancellationToken);
    Task<DomainUser> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
}