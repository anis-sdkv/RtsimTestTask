using AutoBogus;
using Bogus;
using RtsimTestTask.Domain.DomainEntities;

namespace RtsimTestTask.TestingInfrastructure.Fakers;

public static class UserFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<DomainUser> Faker = new AutoFaker<DomainUser>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.Username, f => f.Person.UserName)
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber());

    public static DomainUser Generate()
    {
        lock (Lock)
        {
            return Faker.Generate();
        }
    }
}