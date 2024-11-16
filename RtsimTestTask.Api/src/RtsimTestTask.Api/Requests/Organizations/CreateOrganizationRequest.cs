namespace RtsimTestTask.Api.Requests.Organizations;

public record CreateOrganizationRequest(
    string OrganizationName,
    string? Address,
    string? PhoneNumber
);