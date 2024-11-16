using FluentValidation;
using RtsimTestTask.Api.Requests.Account;
using RtsimTestTask.Domain.Constants;

namespace RtsimTestTask.Api.Validators.Account;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Must(username => username.All(c => UsernameConstants.AllowedUserNameCharacters.Contains(c)))
            .WithMessage("Username must contain only letters, numbers, or the \"_\" character.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(PasswordComplexityContanst.MinPasswordLength)
            .WithMessage($"Password must be at least {PasswordComplexityContanst.MinPasswordLength} characters long.");

        if (PasswordComplexityContanst.RequiresUppercase)
            RuleFor(x => x.Password)
                .Matches(RegexConstants.AtLeastOneUppercase)
                .WithMessage("Password must contain at least one uppercase letter.");

        if (PasswordComplexityContanst.RequiresLowercase)
            RuleFor(x => x.Password)
                .Matches(RegexConstants.AtLeastOneLowercase)
                .WithMessage("Password must contain at least one lowercase letter.");

        if (PasswordComplexityContanst.RequiresDigit)
            RuleFor(x => x.Password)
                .Matches(RegexConstants.AtLeastOneDigit).WithMessage("Password must contain at least one digit.");

        if (PasswordComplexityContanst.RequireNonAlphanumeric)
            RuleFor(x => x.Password)
                .Matches(RegexConstants.AtLeastOneNonAlphanumeric)
                .WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.OrganizationId)
            .NotEmpty().WithMessage("OrganizationId is required.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .Matches(RegexConstants.PhonePattern).When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Invalid phone number format.");
    }
}