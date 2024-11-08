using RtsimTestTask.Core.Abstractions.Repositories;
using RtsimTestTask.Core.DataTransferObjects.RequestModels;
using RtsimTestTask.Core.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class OrganizationsRepository(ApplicationDbContext dbContext)
    : BaseRepository<OrganizationEntity, Guid>(dbContext), IOrganizationsRepository
{
    public Task<DomainOrganization> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DomainOrganization>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(DomainOrganization entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Add(DomainOrganization entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DomainOrganization>> Search(SearchOrganizationDto searchParams, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}