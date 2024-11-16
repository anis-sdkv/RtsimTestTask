using FluentValidation;
using RtsimTestTask.Api.Requests.Account;
using RtsimTestTask.Domain.Constants;

namespace RtsimTestTask.Api.Validators.Account;

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(x => x.Username)
            .Must(username => username.All(c => UsernameConstants.AllowedUserNameCharacters.Contains(c)))
            .WithMessage("Username must contain only letters, numbers, or the \"_\" character.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position is required.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .Matches(RegexConstants.PhonePattern).When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Invalid phone number format.");
    }
}