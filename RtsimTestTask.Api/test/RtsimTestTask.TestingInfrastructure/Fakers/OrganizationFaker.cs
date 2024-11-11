using AutoBogus;
using Bogus;
using RtsimTestTask.Domain.DataTransferObjects;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.TestingInfrastructure.Fakers;

public static class OrganizationFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<DomainOrganization> Faker = new AutoFaker<DomainOrganization>()
        .RuleFor(o => o.Id, f => Guid.NewGuid())
        .RuleFor(o => o.CreatedAt, f => DateTime.UtcNow)
        .RuleFor(o => o.OrganizationName, f => $"{f.Company.CompanyName()}_{Guid.NewGuid()}")
        .RuleFor(o => o.Address, f => f.Address.FullAddress())
        .RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumber());

    private static readonly Faker<CreateOrganizationDto> FakerDto = new AutoFaker<CreateOrganizationDto>()
        .RuleFor(o => o.OrganizationName, f => $"{f.Company.CompanyName()}_{Guid.NewGuid()}")
        .RuleFor(o => o.Address, f => f.Address.FullAddress())
        .RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumber());


    public static DomainOrganization Generate()
    {
        lock (Lock)
        {
            return Faker.Generate();
        }
    }

    public static CreateOrganizationDto GenerateDto()
    {
        lock (Lock)
        {
            return FakerDto.Generate();
        }
    }

    public static DomainOrganization WithId(this DomainOrganization src, Guid id) => src with { Id = id };

    public static IEnumerable<DomainOrganization> Generate(int count) =>
        Enumerable.Range(0, count).Select(_ => Generate());

    public static IEnumerable<CreateOrganizationDto> GenerateDto(int count) =>
        Enumerable.Range(0, count).Select(_ => GenerateDto());
}