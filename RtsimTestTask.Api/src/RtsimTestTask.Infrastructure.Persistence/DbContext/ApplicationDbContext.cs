using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RtsimTestTask.Infrastructure.Persistence.Entities;
using RtsimTestTask.Infrastructure.Persistence.Options;

namespace RtsimTestTask.Infrastructure.Persistence.DbContext;

public class ApplicationDbContext : IdentityDbContext<UserEntity>
{
    public new DbSet<UserEntity> Users { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }
    private readonly IOptions<InfrastructureOptions> _infrastructureOptions;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<InfrastructureOptions> infrastructureOptions) : base(options)
    {
        _infrastructureOptions = infrastructureOptions;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_infrastructureOptions.Value.ConnectionString);
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