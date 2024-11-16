namespace RtsimTestTask.Api.Responses;

public record OrganizationSearchResponse(
    int Page,
    int TotalPages,
    OrganizationResponse[] Organizations
);