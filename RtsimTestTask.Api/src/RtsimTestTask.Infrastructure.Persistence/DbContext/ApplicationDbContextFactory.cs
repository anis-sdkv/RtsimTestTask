using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RtsimTestTask.Infrastructure.Persistence.Options;

namespace RtsimTestTask.Infrastructure.Persistence.DbContext;

// For ef migrations
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
                "RtsimTestTask.Application"))
            .AddJsonFile("appsettings.json")
            .Build();
        var settings = configuration
            .GetSection(nameof(InfrastructureOptions))
            .Get<InfrastructureOptions>() ?? throw new NullReferenceException();
        var infrastructureOptions = Microsoft.Extensions.Options.Options.Create(settings);

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(infrastructureOptions.Value.ConnectionString);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        return new ApplicationDbContext(optionsBuilder.Options, infrastructureOptions);
    }
}