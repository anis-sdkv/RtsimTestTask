using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Domain.Exceptions;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class OrganizationsRepository(ApplicationDbContext context, IMapper mapper)
    : IOrganizationsRepository
{
    public Task<IEnumerable<DomainOrganization>> SearchAsync(SearchOrganizationDto searchParams,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DomainOrganization> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<DomainOrganization?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await context.Organizations
            .FirstOrDefaultAsync(x => x.OrganizationName == name, cancellationToken);
        return mapper.Map<DomainOrganization>(entity);
    }

    public Task UpdateAsync(UpdateOrganizationDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public async Task<Guid> Create(CreateOrganizationDto dto, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<OrganizationEntity>(dto);
        var exist = await context.Organizations
            .FirstOrDefaultAsync(x => x.OrganizationName == dto.OrganizationName, cancellationToken) != null;
        if (exist) throw new OrganizationExistException(dto.OrganizationName);
        context.Organizations.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public Task RemoveAsync(Guid entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}