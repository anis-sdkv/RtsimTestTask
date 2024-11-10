namespace RtsimTestTask.Api.Requests.Organizations;

public record UpdateOrganizationRequest(
    Guid Id,
    string OrganizationName,
    string Address,
    string PhoneNumber,
    Guid OwnerId
);