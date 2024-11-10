// using AutoBogus;
// using Bogus;
// using RtsimTestTask.Domain.DomainEntities;
//
// namespace RtsimTestTask.TestingInfrastructure.Fakers;
//
// public static class OrganizationFaker
// {
//     private static readonly object Lock = new();
//
//     private static readonly Faker<DomainOrganization> Faker = new AutoFaker<DomainOrganization>()
//         .RuleFor(o => o.Id, f => Guid.NewGuid())
//         .RuleFor(o => o.CreatedAt, f => f.Date.Past())
//         .RuleFor(o => o.OrganizationName, f => f.Company.CompanyName())
//         .RuleFor(o => o.Address, f => f.Address.FullAddress())
//         .RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumber())
//         .RuleFor(o => o.OwnerId, f => Guid.NewGuid())
//         .RuleFor(o => o.Employees, f =>
//             Enumerable.Repeat(0, Random.Shared.Next(1, 10)).Select(x => UserFaker.Generate()).ToList());
//
//     public static DomainOrganization Generate()
//     {
//         lock (Lock)
//         {
//             return Faker.Generate();
//         }
//     }
//
//     public static DomainOrganization WithId(this DomainOrganization src, Guid id) => src with { Id = id };
//
//     public static IEnumerable<DomainOrganization> Generate(int count) =>
//         Enumerable.Range(0, count).Select(x => OrganizationFaker.Generate());
// }