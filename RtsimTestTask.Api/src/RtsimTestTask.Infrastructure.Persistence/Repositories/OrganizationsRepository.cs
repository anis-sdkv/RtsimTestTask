using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class OrganizationsRepository(ApplicationDbContext dbContext)
    : BaseRepository<OrganizationEntity, Guid>(dbContext), IOrganizationsRepository
{
    public Task<Organization> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Organization>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Organization entity, CancellationToken cancellationToken)
    {
        base.UpdateAsync(entity., cancellationToken);
        throw new NotImplementedException();
    }

    public void Add(Organization entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}