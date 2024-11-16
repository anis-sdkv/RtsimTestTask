using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.Repositories;

public class UsersRepository(ApplicationDbContext context, IMapper mapper) : IUsersRepository
{
    public async Task<IEnumerable<DomainUser>> GetUsersByOrganizationAsync(
        Guid organizationId,
        CancellationToken cancellationToken)
    {
        var result = await context.Users
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
        return mapper.Map<List<DomainUser>>(result);
    }

    public async Task<DomainUser> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await context.Users
            .AsNoTracking()
            .Where(x => x.Id == id.ToString())
            .SingleOrDefaultAsync(cancellationToken);

        return mapper.Map<DomainUser>(result);
    }

    public async Task<IEnumerable<DomainUser>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var result = await context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return mapper.Map<List<DomainUser>>(result);
    }
}