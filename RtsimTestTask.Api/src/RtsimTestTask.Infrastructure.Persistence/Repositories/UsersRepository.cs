using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class UsersRepository(ApplicationDbContext dbContext) : IUsersRepository
{
    public Task<IEnumerable<DomainUser>> GetUsersByOrganization(Guid organizationId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DomainUser> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}