using FluentValidation;
using RtsimTestTask.Api.Requests.Organizations;

namespace RtsimTestTask.Api.Validators.Organizations;

public class UpdateOrganizationRequestValidator : AbstractValidator<UpdateOrganizationRequest>
{
    public UpdateOrganizationRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Organization Id is required.");

        RuleFor(x => x.OrganizationName)
            .NotEmpty().WithMessage("Organization name is required.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");

        RuleFor(x => x.PhoneNumber)
            .Matches(RegexConstants.AtLeastOneDigit).WithMessage("Invalid phone number format.");
    }
}