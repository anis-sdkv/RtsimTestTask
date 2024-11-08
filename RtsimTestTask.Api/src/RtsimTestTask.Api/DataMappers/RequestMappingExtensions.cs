using RtsimTestTask.Api.Requests;
using RtsimTestTask.Api.Requests.Organizations;
using RtsimTestTask.Core.DomainEntities;

namespace RtsimTestTask.Api.DataMappers;

public static class RequestMappingExtensions
{
    public static User ToDomainUser(this RegisterRequest request) => new User(
        null,
        request.Username ??
        throw new ArgumentNullException(nameof(request.Username), "UserEntity must contain a non-null UserName."),
        request.Position,
        request.OrganizationId
    );
    
    public static void ToDomainOrganization(this CreateOrganizationRequest request) => 
}