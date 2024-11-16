using FluentValidation;
using RtsimTestTask.Api.Requests.Organizations;

namespace RtsimTestTask.Api.Validators.Organizations;

public class CreateOrganizationRequestValidator : AbstractValidator<CreateOrganizationRequest>
{
    public CreateOrganizationRequestValidator()
    {
        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("Organization name is required.");

        RuleFor(x => x.PhoneNumber)
            .Matches(RegexConstants.PhonePattern).When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Invalid phone number format.");
    }
}
