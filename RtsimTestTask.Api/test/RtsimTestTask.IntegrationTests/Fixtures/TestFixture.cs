using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RtsimTestTask.Domain.Abstractions.Repositories;
using RtsimTestTask.Domain.DomainEntities;
using RtsimTestTask.Infrastructure.Persistence.DataMappers;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Entities;
using RtsimTestTask.Infrastructure.Persistence.InfrastructureConfigureExtensions;

namespace RtsimTestTask.IntegrationTests.Fixtures;

public class TestFixture : IDisposable
{
    public readonly IMapper TestMapper;
    public readonly ApplicationDbContext Context;
    public readonly IUsersRepository UsersRepository;
    public readonly IOrganizationsRepository OrganizationsRepository;
    public readonly UserManager<UserEntity> UserManager;


    public TestFixture()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

        var servicesBuilder = RegisterServices();
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<InfrastructureMappingProfile>(); });
        servicesBuilder.AddScoped<IMapper>(_ => config.CreateMapper());
        var services = servicesBuilder.BuildServiceProvider();

        Context = services.GetRequiredService<ApplicationDbContext>();
        TestMapper = services.GetRequiredService<IMapper>();
        UsersRepository = services.GetRequiredService<IUsersRepository>();
        OrganizationsRepository = services.GetRequiredService<IOrganizationsRepository>();
        UserManager = services.GetRequiredService<UserManager<UserEntity>>();
        
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }

    private ServiceCollection RegisterServices()
    {
        var configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.IntegrationTest.json", optional: false, reloadOnChange: false)
            .Build();
        var services = new ServiceCollection();
        services.AddInfrastructure(configurationRoot);
        services.AddAuthentication();
        return services;
    }

    public async Task Seed(IEnumerable<DomainOrganization> organizations)
    {
        var entities = TestMapper.Map<IEnumerable<OrganizationEntity>>(organizations);
        Context.Organizations.AddRange(entities);
        await Context.SaveChangesAsync();
    }

    public async Task Seed(IEnumerable<DomainUser> users)
    {
        var entities = TestMapper.Map<IEnumerable<UserEntity>>(users);
        Context.Users.AddRange(entities);
        await Context.SaveChangesAsync();
    }
}