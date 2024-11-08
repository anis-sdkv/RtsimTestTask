using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class UsersRepository(ApplicationDbContext dbContext) : BaseRepository<UserEntity, Guid>(dbContext), IUsersRepository
{
    public Task<DomainUser> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DomainUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(DomainUser entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Add(DomainUser entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DomainUser>> GetUsersByOrganization(Guid organizationId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}