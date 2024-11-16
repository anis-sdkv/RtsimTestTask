using FluentValidation;
using RtsimTestTask.Api.Requests.Organizations;

namespace RtsimTestTask.Api.Validators.Organizations;

public class SearchOrganizationsRequestValidator : AbstractValidator<SearchOrganizationsRequest>
{
    public SearchOrganizationsRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(0).WithMessage("Page number cannot be negative.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");

        RuleFor(x => x.QueryParams)
            .SetValidator(new QueryParametersValidator()!)
            .When(x => x.QueryParams != null);

        RuleFor(x => x.SortParams)
            .SetValidator(new SortParametersValidator()!)
            .When(x => x.SortParams != null);

        RuleFor(x => x.DateFilterParams)
            .SetValidator(new DateFilterParametersValidator()!)
            .When(x => x.DateFilterParams != null);
    }

    private class QueryParametersValidator : AbstractValidator<SearchOrganizationsRequest.QueryParameters>
    {
        public QueryParametersValidator()
        {
            RuleFor(x => x.Field)
                .NotEmpty().WithMessage("Field is required.");

            RuleFor(x => x.Query)
                .NotEmpty().WithMessage("Query is required.");
        }
    }

    private class SortParametersValidator : AbstractValidator<SearchOrganizationsRequest.SortParameters>
    {
        public SortParametersValidator()
        {
            RuleFor(x => x.SortBy)
                .NotEmpty().WithMessage("SortBy is required.");

            RuleFor(x => x.SortDescending)
                .NotEmpty().WithMessage("SortDescending is required.");
        }
    }

    private class DateFilterParametersValidator : AbstractValidator<SearchOrganizationsRequest.DateFilterParameters>
    {
        public DateFilterParametersValidator()
        {
            RuleFor(x => x.CreatedAfter)
                .LessThan(x => x.CreatedBefore)
                .When(x => x.CreatedAfter.HasValue && x.CreatedBefore.HasValue)
                .WithMessage("CreatedAfter date must be before CreatedBefore date.");
        }
    }
}