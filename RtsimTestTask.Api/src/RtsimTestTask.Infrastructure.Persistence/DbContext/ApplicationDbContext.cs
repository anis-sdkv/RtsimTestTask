using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.DbContext;

public sealed class ApplicationDbContext : IdentityDbContext<UserEntity>
{
    public override DbSet<UserEntity> Users { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
        modelBuilder
            .ConfigureUsersEntity();
        Database.Migrate();
        // .ConfigureOrganizationsEntity();

        base.OnModelCreating(modelBuilder);
    }
}