namespace RtsimTestTask.Api.Responses;

public record OrganizationResponse(
    Guid Id,
    string OrganizationName,
    string Address,
    string PhoneNumber,
    DateTime CreatedAt
);