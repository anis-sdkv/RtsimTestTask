using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class UsersRepository(ApplicationDbContext dbContext) : BaseRepository<User, Guid>(dbContext), IUsersRepository
{
    public Task<IEnumerable<User>> GetUsersByOrganization(Guid organizationId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}