using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RtsimTestTask.Domain.Roles;
using RtsimTestTask.Infrastructure.Persistence.Entities;

namespace RtsimTestTask.Infrastructure.Persistence.DbContext;

public static class ModelBuilderExtensions
{
    private const string PostgresGuidGenFunc = "gen_random_uuid()";
    private const string PostgresCurrentTimestampFunc = "now() at time zone 'utc'";

    public static ModelBuilder ConfigureUsersEntity(this ModelBuilder builder) => builder
        .Entity<UserEntity>(entity =>
        {
            entity.Property(u => u.Id)
                .HasDefaultValueSql(PostgresGuidGenFunc);
            entity.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql(PostgresCurrentTimestampFunc);
            entity.HasIndex(u => u.UserName)
                .IsUnique();
        });


    public static ModelBuilder ConfigureOrganizationsEntity(this ModelBuilder builder) => builder
        .Entity<OrganizationEntity>(entity =>
        {
            entity.Property(o => o.Id)
                .HasDefaultValueSql(PostgresGuidGenFunc);
            entity.Property(o => o.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql(PostgresCurrentTimestampFunc);
            entity.HasIndex(o => o.OrganizationName)
                .IsUnique();
        });
    public static ModelBuilder ConfigureRoles(this ModelBuilder builder) => builder
        .Entity<Role>(entity => { entity.HasData(new Role(DomainRoles.Admin), new Role(DomainRoles.User)); });
    
}