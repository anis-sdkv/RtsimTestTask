namespace RtsimTestTask.Api.Requests.Users;

public class GetUsersRequest(
    string Query,
    int Page,
    int PageSize,
    GetUsersRequest.SortParameters? SortParams,
    GetUsersRequest.DateFilterParameters? DateFilterParams
)
{
    public record SortParameters(
        string SortBy,
        bool SortDescending = false
    );

    public record DateFilterParameters(
        DateTime? CreatedAfter,
        DateTime? CreatedBefore
    );
}