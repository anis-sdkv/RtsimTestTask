namespace RtsimTestTask.Core.DataTransferObjects.RequestModels;

public record SearchOrganizationDto(
    int Page,
    int PageSize,
    SearchOrganizationDto.QueryParameters? QueryParams,
    SearchOrganizationDto.SortParameters? SortParams,
    SearchOrganizationDto.DateFilterParameters? DateFilterParams
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