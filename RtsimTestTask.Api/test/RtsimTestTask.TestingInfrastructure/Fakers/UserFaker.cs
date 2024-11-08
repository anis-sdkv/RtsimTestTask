using AutoBogus;
using Bogus;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.TestingInfrastructure.Fakers;

public static class UserFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<User> Faker = new AutoFaker<User>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.Username, f => f.Person.UserName)
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber());

    public static User Generate()
    {
        lock (Lock)
        {
            return Faker.Generate();
        }
    }
}