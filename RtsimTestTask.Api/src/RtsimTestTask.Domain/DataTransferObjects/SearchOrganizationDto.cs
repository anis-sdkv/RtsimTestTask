namespace RtsimTestTask.Domain.DataTransferObjects;

public record SearchOrganizationDto(
    int Page,
    int PageSize,
    SearchOrganizationDto.QueryParameters? QueryParams = null,
    SearchOrganizationDto.SortParameters? SortParams = null,
    SearchOrganizationDto.DateFilterParameters? DateFilterParams = null
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