using Microsoft.EntityFrameworkCore;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Infrastructure.Persistence.Entities;

public static class ModelBuilderExtensions
{
    private const string PostgresGuidGenFunc = "gen_random_uuid()";
    private const string PostgresCurrentTimestampFunc = "current_timestamp()";

    public static ModelBuilder ConfigureUsersEntity(this ModelBuilder builder) => builder
        .Entity<UserEntity>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Position)
                .IsRequired();

            entity.Property(u => u.OrganizationId)
                .IsRequired();

            entity.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql(PostgresCurrentTimestampFunc);

            entity.HasOne<Organization>()
                .WithMany()
                .HasForeignKey(u => u.OrganizationId);
        });


    public static ModelBuilder ConfigureOrganizationsEntity(this ModelBuilder builder) => builder
        .Entity<OrganizationEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(u => u.Id)
                .HasDefaultValueSql(PostgresGuidGenFunc);
            
            entity.Property(e => e.OrganizationName)
                .IsRequired();

            entity.Property(e => e.Address)
                .HasMaxLength(200);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20); 
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql(PostgresCurrentTimestampFunc); 

            entity.HasMany(e => e.Employees)
                .WithOne(u => u.Ogani) // Предполагается, что у UserEntity есть навигационное свойство Organization
                .HasForeignKey(u => u.OrganizationId) // Внешний ключ в таблице пользователей
                .OnDelete(DeleteBehavior.Cascade); // Поведение при удалении организации
        });
}