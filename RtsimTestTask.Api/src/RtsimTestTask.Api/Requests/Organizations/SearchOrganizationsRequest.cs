namespace RtsimTestTask.Api.Requests.Organizations;

public record SearchOrganizationsRequest(
    int Page,
    int PageSize,
    SearchOrganizationsRequest.QueryParameters? QueryParams,
    SearchOrganizationsRequest.SortParameters? SortParams,
    SearchOrganizationsRequest.DateFilterParameters? DateFilterParams
)
{
    public record QueryParameters(
        string Field,
        string Query
    );

    public record SortParameters(
        string SortBy,
        bool SortDescending = false
    );

    public record DateFilterParameters(
        DateTime? CreatedAfter,
        DateTime? CreatedBefore
    );
}