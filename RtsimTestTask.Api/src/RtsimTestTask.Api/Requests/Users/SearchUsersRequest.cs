namespace RtsimTestTask.Api.Requests.Users;

public record SearchUsersRequest(
    int Page,
    int PageSize,
    SearchUsersRequest.QueryParameters? QueryPrams,
    SearchUsersRequest.SortParameters? SortParams,
    SearchUsersRequest.DateFilterParameters? DateFilterParams
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