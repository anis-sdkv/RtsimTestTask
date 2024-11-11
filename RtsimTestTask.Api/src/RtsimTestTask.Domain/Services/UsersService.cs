using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.Abstractions.Services;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.Domain.Services;

public class UsersService(IUsersRepository users) : IUsersService
{
    public Task<DomainUser> GetByIdAsync(Guid userId, CancellationToken cancellationToken) =>
        users.GetUserByIdAsync(userId, cancellationToken);
}
