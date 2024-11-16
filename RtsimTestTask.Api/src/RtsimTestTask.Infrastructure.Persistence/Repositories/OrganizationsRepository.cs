using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
    private readonly DbSet<OrganizationEntity> _organizations = context.Organizations;

    public async Task<IEnumerable<DomainOrganization>> SearchAsync(SearchOrganizationDto searchParams,
        CancellationToken cancellationToken)
    {
        var query = _organizations.AsQueryable();

        if (searchParams.QueryParams is not null)
        {
            var field = searchParams.QueryParams.Field;
            var queryText = searchParams.QueryParams.Query;

            query = query.Where(org => EF.Property<string>(org, field).Contains(queryText));
        }

        if (searchParams.DateFilterParams is not null)
        {
            if (searchParams.DateFilterParams.CreatedAfter.HasValue)
                query = query.Where(org => org.CreatedAt > searchParams.DateFilterParams.CreatedAfter.Value);

            if (searchParams.DateFilterParams.CreatedBefore.HasValue)
                query = query.Where(org => org.CreatedAt < searchParams.DateFilterParams.CreatedBefore.Value);
        }

        if (searchParams.SortParams is not null)
        {
            var sortBy = searchParams.SortParams.SortBy;
            var sortDescending = searchParams.SortParams.SortDescending;

            query = sortDescending
                ? query.OrderByDescending(org => EF.Property<object>(org, sortBy))
                : query.OrderBy(org => EF.Property<object>(org, sortBy));
        }

        var queryResult = await query
            .Skip(searchParams.PageSize * searchParams.Page)
            .Take(searchParams.PageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<List<DomainOrganization>>(queryResult);
    }

    public async Task<IEnumerable<DomainOrganization>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _organizations
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);
        return mapper.Map<List<DomainOrganization>>(result);
    }

    public async Task<DomainOrganization?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _organizations
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        return mapper.Map<DomainOrganization>(entity);
    }

    public async Task<DomainOrganization?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await _organizations
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.OrganizationName == name, cancellationToken);
        return mapper.Map<DomainOrganization>(entity);
    }

    public async Task UpdateAsync(UpdateOrganizationDto dto, CancellationToken cancellationToken)
    {
        var entity = await _organizations
            .SingleOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);
        if (entity == null) throw new EntityNotFoundException(dto.Id);

        entity.OrganizationName = dto.OrganizationName;
        entity.Address = dto.Address;
        entity.PhoneNumber = dto.PhoneNumber;

        _organizations.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Guid> CreateAsync(CreateOrganizationDto dto, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<OrganizationEntity>(dto);
        var exist = await _organizations
            .FirstOrDefaultAsync(x => x.OrganizationName == dto.OrganizationName, cancellationToken) != null;
        if (exist) throw new OrganizationExistException(dto.OrganizationName);
        _organizations.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _organizations
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity == null) throw new EntityNotFoundException(id);
        _organizations.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}