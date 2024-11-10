using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RtsimTestTask.Infrastructure.Persistence.Entities;
using RtsimTestTask.Infrastructure.Persistence.Options;

namespace RtsimTestTask.Infrastructure.Persistence.DbContext;

public sealed class ApplicationDbContext : IdentityDbContext<UserEntity>
{
    public new DbSet<UserEntity> Users { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }
    private readonly IOptions<InfrastructureOptions> _infrastructureConfig;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<InfrastructureOptions> infrastructureConfig) : base(options)
    {
        _infrastructureConfig = infrastructureConfig;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_infrastructureConfig.Value.ConnectionString);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ConfigureOrganizationsEntity()
            .ConfigureUsersEntity()
            .ConfigureRoles();

        base.OnModelCreating(modelBuilder);
    }
}